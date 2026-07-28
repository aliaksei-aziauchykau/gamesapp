import {
    ChangeDetectionStrategy,
    Component,
    ContentChild,
    HostListener,
    inject,
    Input,
    OnDestroy,
    OnInit,
    TemplateRef,
} from '@angular/core';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { IGameContainerState } from '@itdl/game-portal/features/game-container/entities';
import { GameContainerXSState } from '@itdl/game-portal/features/game-container/ng-xs';
import { GameContainerSocketActions } from '@itdl/game-portal/features/game-container/socket';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { Store } from '@ngxs/store';
import { RxState, rxState } from '@rx-angular/state';
import { delay, interval, map, merge, scan, startWith, takeUntil } from 'rxjs';

type State = { gameContainerState: IGameContainerState; gameStateName: string | null };

@Component({
    selector: 'gpll-game-container',
    templateUrl: './game-container.component.html',
    styleUrls: ['./game-container.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [RxState],
})
export class GpllGameContainerComponent implements OnInit, OnDestroy {
    @ContentChild('onContainerRunning') onContainerRunning!: TemplateRef<unknown>;
    @ContentChild('onContainerCreated') onContainerCreated!: TemplateRef<unknown>;

    @Input() public readonly isPanelHidden!: string;

    private readonly uiActions = inject(ShrlUiActionsService);
    private readonly store = inject(Store);

    private readonly gameCode = this.store.selectSnapshot(GameXSState.gameCode);
    private readonly userId = this.store.selectSnapshot(UserXSState.userIdSafety);

    private readonly connect = (gameId: string, userId: string) =>
        this.uiActions.dispatch(new GameContainerSocketActions.Connect(gameId, userId));

    private readonly disconnect = (gameId: string, userId: string) =>
        this.uiActions.dispatch(new GameContainerSocketActions.Disconnect(gameId, userId));

    private readonly state = rxState<State>(({ connect }) => {
        connect('gameContainerState', this.store.select(GameContainerXSState.gameContainerState));
        connect('gameStateName', this.store.select(GameContainerXSState.currentGameStateName));
    });

    public readonly isRunningClass$ = this.state.select('gameContainerState').pipe(
        delay(1500),
        map((gameContainerState) => (gameContainerState.isRunning ? 'text-green-400' : 'text-red-400')),
    );

    public readonly isCreated$ = this.state
        .select('gameContainerState')
        .pipe(map((gameContainerState) => gameContainerState.isCreated));

    public readonly blinking$ = interval(250).pipe(
        startWith(1),
        map(() => ''),
        scan((_, __, index) => (index % 2 === 0 ? '' : 'text-cyan-400')),
        takeUntil(this.isRunningClass$),
    );

    public readonly isRunning$ = this.isRunningClass$.pipe(map((isRunning) => isRunning === 'text-green-400'));
    public readonly isRunningIndicator$ = merge(this.isRunningClass$, this.blinking$);
    public readonly gameStateName$ = this.state.select('gameStateName').pipe(isNotNullOrUndefined());

    public ngOnInit(): void {
        this.connect(this.gameCode, this.userId);
    }

    @HostListener('window:beforeunload', ['$event'])
    public ngOnDestroy(): void {
        this.disconnect(this.gameCode, this.userId);
    }
}
