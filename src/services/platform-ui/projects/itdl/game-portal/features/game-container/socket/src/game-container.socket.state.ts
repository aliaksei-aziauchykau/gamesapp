import { inject, Injectable } from '@angular/core';
import { IGameContainerState, IGameContext } from '@itdl/game-portal/features/game-container/entities';
import { GameContainerStoreActions } from '@itdl/game-portal/features/game-container/ng-xs';
import { GameContainerUiActions } from '@itdl/game-portal/features/game-container/ui-actions';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { processAction, ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { ShrlSocketConnectionProvider } from '@itdl/shared/signalr';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { map, merge, of, switchMap, tap } from 'rxjs';

import { GameContainerSocketActions } from './game-container.socket.actions';
import { GameContainerSocketCommands } from './game-container.socket.commands';

@Injectable()
export class GpllGameContainerSocketState {
    private readonly socketConnectionProvider = inject(ShrlSocketConnectionProvider);
    private readonly store = inject(Store);
    private readonly uiActions = inject(ShrlUiActionsService);
    private readonly state = inject(RxState<object>);
    private readonly connection$ = this.socketConnectionProvider.connection$;
    private readonly handleAction = this.uiActions.getSocketActionHandler(this.connection$);

    constructor() {
        this.state.hold(
            merge(
                // to client
                this.gameContainerState$,
                this.gameContext$,
                this.gameSateChanged$,
                // to server
                this.create$,
                this.initialize$,
                this.start$,
                this.stop$,
                this.destroy$,
                this.fetchGameContainerState$,
                this.fetchGameContainerGameContext$,
                this.connect$,
                this.disconnect$,
                // composite
                this.run$,
            ),
        );
    }

    // To Client
    private readonly gameContainerState$ = this.connection$.pipe(
        switchMap((hub) => hub.on<IGameContainerState>(GameContainerSocketCommands.ToClient.GameContainerState)),
        tap((gameContainerState) => this.store.dispatch(new GameContainerStoreActions.Store(gameContainerState))),
    );

    private readonly gameContext$ = this.connection$.pipe(
        switchMap((hub) => hub.on<IGameContext>(GameContainerSocketCommands.ToClient.GameContext)),
        tap((gameContext) => this.store.dispatch(new GameContainerStoreActions.StoreContainerGameContext(gameContext))),
    );

    private readonly gameSateChanged$ = this.connection$.pipe(
        switchMap((hub) => hub.on<string>(GameContainerSocketCommands.ToClient.GameStateChanged)),
        tap((gameState) => this.store.dispatch(new GameContainerStoreActions.StoreGameStateChanged(gameState))),
    );

    // To Server
    private readonly create$ = this.handleAction(GameContainerSocketActions.Create, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.Create, action.gameDefName, action.gameId),
    );

    private readonly start$ = this.handleAction(GameContainerSocketActions.Start, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.Start, action.gameId),
    );

    private readonly initialize$ = this.handleAction(GameContainerSocketActions.Initialize, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.Initialize, action.gameId),
    );

    private readonly stop$ = this.handleAction(GameContainerSocketActions.Stop, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.Stop, action.gameId),
    );

    private readonly destroy$ = this.handleAction(GameContainerSocketActions.Destroy, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.Destroy, action.gameId),
    );

    private readonly connect$ = this.handleAction(GameContainerSocketActions.Connect, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.ConnectCommand, action),
    );

    private readonly disconnect$ = this.handleAction(GameContainerSocketActions.Disconnect, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.DisconnectCommand, action),
    );

    private readonly fetchGameContainerState$ = this.handleAction(GameContainerSocketActions.Fetch, ([action, hub]) =>
        hub.send(GameContainerSocketCommands.ToServer.GetState, action.gameId),
    );

    private readonly fetchGameContainerGameContext$ = this.handleAction(
        GameContainerSocketActions.FetchContainerGameContext,
        ([action, hub]) => hub.send(GameContainerSocketCommands.ToServer.GetGameContext, action.gameId),
    );

    // Composite
    private readonly run$ = this.uiActions.action$(GameContainerUiActions.Run).pipe(
        isNotNullOrUndefined(),
        switchMap((actionMeta) => {
            const { gameDefName, gameId } = actionMeta.action;

            const chain = of({}).pipe(
                switchMap(() => this.uiActions.dispatch(new GameContainerSocketActions.Create(gameDefName, gameId))),
                switchMap(() => this.uiActions.dispatch(new GameContainerSocketActions.Initialize(gameId))),
                switchMap(() => this.uiActions.dispatch(new GameContainerSocketActions.Start(gameId))),
                switchMap(() => this.uiActions.dispatch(new GameContainerSocketActions.Fetch(gameId))),
                map(() => actionMeta),
            );

            return chain;
        }),
        processAction(),
    );
}
