import { inject, Injectable } from '@angular/core';
import {
    KrokoGameSocketActions,
    KrokoGameStoreActions,
    KrokoGameUiActions,
} from '@itdl/game-portal/features/games/kroko-game/actions';
import {
    IKrokoGameContext,
    IKrokoGameSettings,
    IKrokoPlayerGameContext,
} from '@itdl/game-portal/features/games/kroko-game/entities';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { ShrlSocketConnectionProvider } from '@itdl/shared/signalr';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { merge, switchMap, tap } from 'rxjs';

import { KrokoGameSocketCommands } from './kroko-game.socket.commands';

@Injectable()
export class GpllKrokoGameSocketState {
    private readonly shrl = inject(ShrlSocketConnectionProvider);
    private readonly connection$ = this.shrl.connection$;
    private readonly store = inject(Store);
    private readonly uiActions = inject(ShrlUiActionsService);
    private readonly state = inject(RxState<object>);

    private readonly handleAction = this.uiActions.getSocketActionHandler(this.connection$);

    constructor() {
        this.state.hold(
            merge(
                // to client
                this.gameContext$,
                this.gameSettings$,
                this.playerGameContext$,
                this.selectWord$,
                this.closeAllModalWindow$,
                this.rejoinPlayer$,
                this.reloadShapes$,
                // to server
                this.joinPlayer$,
                this.leavePlayer$,
                this.guessWord$,
                this.drawWord$,
                this.storeSelectedWord$,
                this.fetchGameContext$,
                this.fetchGameSettings$,
                this.saveGameSettings$,
                this.startGame$,
            ),
        );
    }

    // To Client
    private readonly gameContext$ = this.connection$.pipe(
        switchMap((hub) => hub.on<IKrokoGameContext>(KrokoGameSocketCommands.ToClient.GameContext)),
        tap((gameContext) => this.store.dispatch(new KrokoGameStoreActions.StoreGameContext(gameContext))),
    );

    private readonly gameSettings$ = this.connection$.pipe(
        switchMap((hub) => hub.on<IKrokoGameSettings>(KrokoGameSocketCommands.ToClient.GameSettings)),
        tap((gameSettings) => this.store.dispatch(new KrokoGameStoreActions.StoreGameSettings(gameSettings))),
    );

    private readonly playerGameContext$ = this.connection$.pipe(
        switchMap((hub) => hub.on<IKrokoPlayerGameContext>(KrokoGameSocketCommands.ToClient.PlayerGameContext)),
        tap((playerGameContext) =>
            this.store.dispatch(new KrokoGameStoreActions.StorePlayerGameContext(playerGameContext)),
        ),
    );

    private readonly reloadShapes$ = this.connection$.pipe(
        switchMap((hub) => hub.on<void>(KrokoGameSocketCommands.ToClient.ReloadShapes)),
        tap(() => this.uiActions.dispatch(new KrokoGameUiActions.ReloadShapesUiAction())),
    );

    private readonly selectWord$ = this.connection$.pipe(
        switchMap((hub) => hub.on<string[]>(KrokoGameSocketCommands.ToClient.SelectWord)),
        tap((words) => this.uiActions.dispatch(new KrokoGameUiActions.SelectWordUiAction(words))),
    );

    private readonly closeAllModalWindow$ = this.connection$.pipe(
        switchMap((hub) => hub.on<IKrokoGameContext>(KrokoGameSocketCommands.ToClient.CloseAllModals)),
        tap(() => this.uiActions.dispatch(new KrokoGameUiActions.CloseAllModalsUiAction())),
    );

    private readonly rejoinPlayer$ = this.connection$.pipe(
        switchMap((hub) => hub.onReconnected$()),
        tap(() => this.uiActions.dispatch(new KrokoGameUiActions.RejoinPlayerUiAction())),
    );

    // To Server
    private readonly joinPlayer$ = this.handleAction(KrokoGameSocketActions.JoinPlayer, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.JoinPlayer, action),
    );

    private readonly leavePlayer$ = this.handleAction(KrokoGameSocketActions.LeavePlayer, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.LeavePlayer, action),
    );

    private readonly guessWord$ = this.handleAction(KrokoGameSocketActions.GuessWord, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.GuessWord, action),
    );

    private readonly drawWord$ = this.handleAction(KrokoGameSocketActions.DrawWord, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.DrawWord, action),
    );

    private readonly storeSelectedWord$ = this.handleAction(KrokoGameSocketActions.StoreSelectedWord, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.StoreSelectedWord, action),
    );

    private readonly saveGameSettings$ = this.handleAction(KrokoGameSocketActions.SaveGameSettings, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.SaveGameSettings, action),
    );

    private readonly fetchGameContext$ = this.handleAction(KrokoGameSocketActions.FetchGameContext, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.FetchGameContext, action),
    );

    private readonly fetchGameSettings$ = this.handleAction(KrokoGameSocketActions.FetchGameSettings, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.FetchGameSettings, action),
    );

    private readonly startGame$ = this.handleAction(KrokoGameSocketActions.StartGame, ([action, hub]) =>
        hub.send(KrokoGameSocketCommands.ToServer.StartGame, action),
    );

    // private readonly fetchGameContext$ = this.uiActions.action$(KrokoGameSocketActions.FetchGameContext).pipe(
    //     isNotNullOrUndefined(),
    //     withLatestFrom(this.connection$),
    //     switchMap(([actionMeta, hub]) =>
    //         hub
    //             .invoke<IKrokoGameContext>(KrokoGameSocketCommands.ToServer.FetchGameContext, actionMeta.action.gameId)
    //             .pipe(map((gameContext) => ({ gameContext, actionMeta }))),
    //     ),
    //     switchMap(({ gameContext, actionMeta }) =>
    //         this.store.dispatch(new KrokoGameStoreActions.StoreGameContext(gameContext)).pipe(map(() => actionMeta)),
    //     ),
    //     processAction(),
    // );

    // private readonly fetchGameSettings$ = this.uiActions.action$(KrokoGameSocketActions.FetchGameSettings).pipe(
    //     isNotNullOrUndefined(),
    //     withLatestFrom(this.connection$),
    //     switchMap(([actionMeta, hub]) =>
    //         hub
    //             .invoke<IKrokoGameSettings>(
    //                 KrokoGameSocketCommands.ToServer.FetchGameSettings,
    //                 actionMeta.action.gameId,
    //             )
    //             .pipe(map((gameSettings) => ({ gameSettings, actionMeta }))),
    //     ),
    //     switchMap(({ gameSettings, actionMeta }) =>
    //         this.store.dispatch(new KrokoGameStoreActions.StoreGameSettings(gameSettings)).pipe(map(() => actionMeta)),
    //     ),
    //     processAction(),
    // );
}
