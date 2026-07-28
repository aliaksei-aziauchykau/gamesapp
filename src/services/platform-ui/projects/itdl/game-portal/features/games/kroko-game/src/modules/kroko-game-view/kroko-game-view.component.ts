import { ChangeDetectionStrategy, Component, HostListener, inject, Injector, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import {
    DrawToolStateActions,
    KrokoGameSocketActions,
    KrokoGameUiActions,
} from '@itdl/game-portal/features/games/kroko-game/actions';
import {
    DrawToolState,
    IDrawToolConfig,
    IKrokoGameContext,
} from '@itdl/game-portal/features/games/kroko-game/entities';
import { KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { PlayerXSState } from '@itdl/game-portal/features/player/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { fade } from '@itdl/shared/animations';
import { IInjectorProvider, isNotNullOrUndefined, ServerSideSkip } from '@itdl/shared/common';
import { processAction, ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { filter, merge, switchMap, tap } from 'rxjs';

import { GpllKrokoGameActionsService } from '../../services/kroko-game.actions.service';
import { GpllJoinPlayerDialogService } from '../join-player-dialog/join-player-dialog.service';
import { GpllDrawToolConfigService } from '../kroko-board/services/draw-tool-config.service';
import { GpllDrawToolStateService } from '../kroko-board/services/draw-tool-state.service';
import { GpllSelectWordDialogService } from '../select-word-dialog/select-word-dialog.service';

type State = {
    drawToolConfig: IDrawToolConfig;
    gameContext: IKrokoGameContext;
    isWinner: boolean;
};

@Component({
    selector: 'gpll-kroko-game-view',
    templateUrl: './kroko-game-view.component.html',
    styleUrls: ['./kroko-game-view.component.scss'],
    providers: [RxState],
    animations: [fade],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllKrokoGameViewComponent implements IInjectorProvider, OnInit, OnDestroy {
    private readonly actions = inject(GpllKrokoGameActionsService);
    private readonly uiActions = inject(ShrlUiActionsService);

    public gameContext$ = this.state.select('gameContext');
    public drawToolConfig$ = this.state.select('drawToolConfig');
    private readonly isWinner$ = this.state.select('isWinner');
    public date = new Date();
    public a = this.date.setMinutes(this.date.getMinutes() + 20);
    public newDate = new Date(this.date);

    constructor(
        private readonly joinPlayerDialogService: GpllJoinPlayerDialogService,
        private readonly selectWordDialogService: GpllSelectWordDialogService,
        private readonly drawToolStateService: GpllDrawToolStateService,
        public readonly injector: Injector,
        private readonly store: Store,
        private readonly state: RxState<State>,
        private readonly drawToolConfigService: GpllDrawToolConfigService,
        private readonly dialog: MatDialog,
    ) {}

    private readonly gameCode = this.store.selectSnapshot(GameXSState.gameCode);
    private readonly userId = this.store.selectSnapshot(UserXSState.userIdSafety);
    private readonly nickname = this.store.selectSnapshot(PlayerXSState.playerNickname);

    private readonly joinPlayer = this.actions.getJoinPlayerActionFn(this.gameCode, this.userId);
    private readonly leavePlayer = this.actions.getLeavePlayerActionFn(this.gameCode, this.userId);
    private readonly storeSelectedWord = this.actions.getStoreSelectedWordActionFn(this.gameCode, this.userId);
    private readonly fetchGameContext = this.actions.getGameContextActionFn(this.gameCode);

    private readonly playSoundForWinner$ = this.isWinner$.pipe(
        isNotNullOrUndefined(),
        filter((isWinner) => !!isWinner),
        tap(() => {
            const audio = new Audio('/assets/audio/ShortVictorySound.mp3');
            audio.load();
            audio.play();
        }),
    );

    private readonly selectWordUiAction$ = this.uiActions.action$(KrokoGameUiActions.SelectWordUiAction).pipe(
        isNotNullOrUndefined(),
        processAction(),
        tap(() => {
            const audio = new Audio('/assets/audio/MixWaterDropBellSound2.mp3');
            audio.load();
            audio.play();
        }),
        switchMap(({ action }) => this.selectWordDialogService.open(action.words).afterClosed()),
        filter((word) => !!word),
        tap((word) => this.storeSelectedWord(word)),
    );

    private readonly closeAllWordUiAction$ = this.uiActions.action$(KrokoGameUiActions.CloseAllModalsUiAction).pipe(
        isNotNullOrUndefined(),
        tap(() => this.dialog.closeAll()),
        processAction(),
    );

    private readonly rejoinPlayerUiAction$ = this.uiActions.action$(KrokoGameUiActions.RejoinPlayerUiAction).pipe(
        isNotNullOrUndefined(),
        tap(() => this.joinPlayer(this.nickname)),
        processAction(),
    );

    private readonly reloadShapesUiAction$ = this.uiActions.action$(KrokoGameUiActions.ReloadShapesUiAction).pipe(
        isNotNullOrUndefined(),
        tap(() => this.drawToolStateService.next({ shapes: [] })),
        tap(() => this.store.dispatch(new DrawToolStateActions.StoreInitialState(new DrawToolState([])))),
        processAction(),
    );

    public ngOnInit(): void {
        this.state.connect('drawToolConfig', this.drawToolConfigService.value$.pipe(isNotNullOrUndefined()));
        this.state.connect(
            'gameContext',
            this.store.select(KrokoGameXSState.gameContext).pipe(
                isNotNullOrUndefined(),
                filter((gameContext) => gameContext.gameId === this.gameCode),
            ),
        );
        this.state.hold(
            merge(
                this.selectWordUiAction$,
                this.closeAllWordUiAction$,
                this.rejoinPlayerUiAction$,
                this.reloadShapesUiAction$,
                this.playSoundForWinner$,
            ),
        );

        this.state.connect('isWinner', this.store.select(KrokoGameXSState.isWinner));

        this.fetchGameContext();
        this.joinPlayer(this.nickname);
        this.uiActions.dispatch(new KrokoGameSocketActions.StartGame(this.gameCode));
    }

    @HostListener('window:beforeunload', ['$event'])
    public ngOnDestroy(): void {
        this.leavePlayer();
    }

    @ServerSideSkip()
    private openJoinPlayerDialog() {
        this.joinPlayerDialogService
            .openJoinPlayerDialog()
            .afterClosed()
            .pipe
            // switchMap((data) => this.signalrService.createRxHubConnection('chatHub').invoke('StartServer', 'test')),
            // tap(() => this.openJoinPlayerDialog()),
            ()
            .subscribe();
    }
}
