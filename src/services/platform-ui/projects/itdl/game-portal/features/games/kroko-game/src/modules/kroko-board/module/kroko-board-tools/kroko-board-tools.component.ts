import { Component, inject, Input, OnInit } from '@angular/core';
import { DrawToolType, IDrawToolConfig } from '@itdl/game-portal/features/games/kroko-game/entities';
import { KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { GpllFormControl, GpllFormGroup } from '@itdl/game-portal/forms';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { CustomSyncModel } from '@itdl/shared/forms/form-data-sync';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { map } from 'rxjs';

import { GpllDrawToolConfigService } from '../../services/draw-tool-config.service';
import { GpllDrawToolStateService } from '../../services/draw-tool-state.service';

type State = {
    timer: string | undefined;
};

@Component({
    selector: 'gpll-kroko-board-tools',
    templateUrl: 'kroko-board-tools.component.html',
    styleUrls: ['./kroko-board-tools.component.html'],
    providers: [RxState],
})
export class GpllKrokoBoardToolsComponent implements OnInit {
    @Input() config!: IDrawToolConfig;

    public strokeColorControl!: GpllFormControl<string>;
    public fillColorControl!: GpllFormControl<string>;
    private readonly drawToolConfigService = inject(GpllDrawToolConfigService);
    private readonly drawToolStateService = inject(GpllDrawToolStateService);

    public readonly isUndoDisabled$ = this.drawToolStateService.isUndoAvailable$.pipe(map((x) => !x));
    public readonly isRedoDisabled$ = this.drawToolStateService.isRedoAvailable$.pipe(map((x) => !x));

    public timer$ = this.state.select('timer').pipe(map((date) => (date ? new Date(date) : undefined)));

    public form!: GpllFormGroup;

    public readonly toolIcons: Record<DrawToolType, string> = {
        pen: 'brush',
        circle: 'circle',
        donught: 'trip_origin',
        rectangle: 'rectangle',
        eraser: 'eraser',
    };

    constructor(private readonly state: RxState<State>, private readonly store: Store) {
        this.state.connect('timer', this.store.select(KrokoGameXSState.timerDate));
    }

    public ngOnInit(): void {
        this.form = this.getForm(this.config);
    }

    public toolIcon$ = this.drawToolConfigService.value$.pipe(
        isNotNullOrUndefined(),
        map((x) => this.toolIcons[x.tool]),
    );

    public clear() {
        this.drawToolStateService.clear().addToHistory();
    }

    public undo() {
        this.drawToolStateService.undo();
    }

    public redo() {
        this.drawToolStateService.redo();
    }

    public setTool(tool: DrawToolType) {
        this.drawToolConfigService.next({ ...this.drawToolConfigService.valueSafe, tool });
    }

    private getForm(config: IDrawToolConfig) {
        this.strokeColorControl = this.getStrokeColorControl(config);
        this.fillColorControl = GpllFormControl.create<string>({ formState: config.fillColor });

        return GpllFormGroup.create({
            controls: {
                strokeColor: this.strokeColorControl,
                fillColor: this.fillColorControl,
            },
        });
    }

    private getFillColorControl(config: IDrawToolConfig) {
        return GpllFormControl.create<string>({ formState: config.fillColor });
    }

    private getStrokeColorControl(config: IDrawToolConfig) {
        return GpllFormControl.create<string>({
            formState: config.strokeColor,
            formDataSyncModel: CustomSyncModel.create({
                handler: ({ currentValue }) =>
                    this.drawToolConfigService.next({
                        ...this.drawToolConfigService.valueSafe,
                        strokeColor: currentValue,
                    }),
            }),
        });
    }
}
