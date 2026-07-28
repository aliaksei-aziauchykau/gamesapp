import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, Input, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { DrawToolStateActions } from '@itdl/game-portal/features/games/kroko-game/actions';
import { DrawToolState, IDrawToolContext } from '@itdl/game-portal/features/games/kroko-game/entities';
import { KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { GpllCardModule } from '@itdl/game-portal/ui/card';
import { isInputNotNullOrUndefined, isNotNullOrUndefined } from '@itdl/shared/common';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import {
    ShrlKonvaCircleComponent,
    ShrlKonvaLayerComponent,
    ShrlKonvaLineComponent,
    ShrlKonvaStageComponent,
} from '@itdl/shared/ui/konva';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import { combineLatest, debounceTime, filter, map, merge, skip, take, tap, withLatestFrom } from 'rxjs';

import { GpllKrokoGameActionsService } from '../../services/kroko-game.actions.service';
import { GpllDrawToolDirective } from './directives/draw-tool.directive';
import { GpllDrawToolConfigService } from './services/draw-tool-config.service';
import { GpllDrawToolStageService } from './services/draw-tool-stage.service';
import { GpllDrawToolStateService } from './services/draw-tool-state.service';
import { GpllTestCanvasComponent } from './test/test-canvas.component';
import { GpllCircleDrawToolService } from './tools/circle-draw-tool.service';
import { GpllDonughtDrawToolService } from './tools/donught-draw-tool.service';
import { GpllDrawToolFactory } from './tools/draw-tool.factory';
import { GpllLineDrawToolService } from './tools/line-draw-tool.service';

type State = {
    context: IDrawToolContext;
    isDrawer: boolean;
    currentRoundShapesParsed: any[];
};

@Component({
    selector: 'gpll-kroko-board',
    templateUrl: './kroko-board.component.html',
    styleUrls: ['./kroko-board.component.scss'],
    imports: [
        CommonModule,
        ShrlKonvaStageComponent,
        ShrlKonvaLayerComponent,
        MatCardModule,
        ShrlKonvaCircleComponent,
        GpllCardModule,
        ShrlKonvaLineComponent,
        GpllDrawToolDirective,
        GpllTestCanvasComponent,
        RxLet,
    ],
    standalone: true,
    providers: [
        GpllDrawToolFactory,
        //
        GpllLineDrawToolService,
        GpllCircleDrawToolService,
        GpllDonughtDrawToolService,
        //
        RxState,
    ],
    changeDetection: ChangeDetectionStrategy.Default,
})
export class GpllKrokoBoardComponent implements OnInit {
    @Input() container!: HTMLElement;

    private readonly state: RxState<State> = inject(RxState<State>);
    private readonly store = inject(Store);
    private readonly krokoGameActionsService = inject(GpllKrokoGameActionsService);
    private readonly uiActions = inject(ShrlUiActionsService);

    private readonly drawToolFactory = inject(GpllDrawToolFactory);
    private readonly drawToolConfigService = inject(GpllDrawToolConfigService);
    private readonly drawToolStateService = inject(GpllDrawToolStateService);
    private readonly drawToolStageService = inject(GpllDrawToolStageService);
    private readonly cdr = inject(ChangeDetectorRef);

    private readonly drawToolConfig$ = this.drawToolConfigService.value$.pipe(isNotNullOrUndefined());
    private readonly drawToolStage$ = this.drawToolStageService.value$.pipe();
    private readonly drawToolState$ = this.drawToolStateService.value$.pipe(isNotNullOrUndefined());

    private readonly currentRoundShapesParsed$ = this.state.select('currentRoundShapesParsed');

    public context$ = this.state.select('context');
    public isDrawer$ = this.state.select('isDrawer');
    public detect$ = this.drawToolState$.pipe(tap(() => this.cdr.markForCheck()));

    public ngOnInit(): void {
        this.state.connect('isDrawer', this.store.select(KrokoGameXSState.isDrawer));
        this.state.connect('currentRoundShapesParsed', this.store.select(KrokoGameXSState.currentRoundShapesParsed));

        this.state.connect('context', this.drawToolContext$);
        // this.state.connect('loadedShapes', this.loadShapes$);

        this.state.hold(this.shapesSource$);
        this.state.hold(this.shapesConsume$);
        this.state.hold(this.loadShapes$);

        // new Stage()
        // this.state.hold(this.detect$);
    }

    private drawWordAction = this.krokoGameActionsService.getDrawWordActionFn(
        this.store.selectSnapshot(GameXSState.gameCode),
        this.store.selectSnapshot(UserXSState.userIdSafety),
    );

    private get loadShapes$() {
        const source = this.currentRoundShapesParsed$.pipe(
            take(1),
            tap((shapes) => this.drawToolStateService.next({ shapes })),
            tap((shapes) => this.store.dispatch(new DrawToolStateActions.StoreInitialState(new DrawToolState(shapes)))),
        );

        return source;
    }

    private get shapesSource$() {
        const source = combineLatest([
            this.store.select(KrokoGameXSState.currentRoundShapesParsed),
            this.isDrawer$,
        ]).pipe(
            filter(([_, isDrawer]) => isDrawer === false),
            tap(([shapes]) => this.drawToolStateService.next({ shapes })),
        );
        return source;
    }

    private get shapesConsume$() {
        const source = merge(
            this.drawToolState$.pipe(
                skip(1),
                map((state) => state.shapes),
            ),
        ).pipe(
            withLatestFrom(this.isDrawer$),
            filter(([_, isDrawer]) => isDrawer),
            debounceTime(100),
            filter(([shapes]) => isInputNotNullOrUndefined(shapes)),
            tap(([shapes]) => this.drawWordAction(JSON.stringify(shapes))),
        );
        return source;
    }

    private get drawToolContext$() {
        const source = combineLatest([
            this.drawToolConfig$,
            this.drawToolState$,
            this.drawToolStage$,
            this.isDrawer$,
        ]).pipe(
            // debounceTime(50),
            map(([config, state, stage, isDrawer]) => ({
                config,
                state,
                stage,
                tool: isDrawer ? this.drawToolFactory.get(config.tool) : null,
            })),
        );

        return source;
    }

    public trackByFn(_: number, item: { id: string }) {
        return item.id;
    }
}
