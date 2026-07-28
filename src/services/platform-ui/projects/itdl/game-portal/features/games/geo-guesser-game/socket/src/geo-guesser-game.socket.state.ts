import { inject, Injectable } from '@angular/core';
import { IGeoGuesserGameContext } from '@itdl/game-portal/features/games/geo-guesser-game/entities';
import { GpllGeoGuesserStoreActions } from '@itdl/game-portal/features/games/geo-guesser-game/ng-xs';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { ShrlSocketConnectionProvider } from '@itdl/shared/signalr';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { merge, switchMap, tap } from 'rxjs';

import { GpllGeoGuesserGameSocketActions } from './geo-guesser-game.socket.actions';
import { GpllGeoGuesserGameSocketCommands } from './geo-guesser-game.socket.commands';

@Injectable()
export class GpllGeoGuesserGameSocketState {
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
                // to server
                this.joinPlayer$,
                this.leavePlayer$,
                this.startGame$,
                this.fetchGameContext$,
            ),
        );
    }

    // To Client
    private readonly gameContext$ = this.connection$.pipe(
        switchMap((hub) => hub.on<IGeoGuesserGameContext>(GpllGeoGuesserGameSocketCommands.ToClient.GameContext)),
        tap((gameContext) => this.store.dispatch(new GpllGeoGuesserStoreActions.StoreGameContext(gameContext))),
    );

    // To Server
    private readonly joinPlayer$ = this.handleAction(GpllGeoGuesserGameSocketActions.JoinPlayer, ([action, hub]) =>
        hub.send(GpllGeoGuesserGameSocketCommands.ToServer.JoinPlayer, action),
    );

    private readonly leavePlayer$ = this.handleAction(GpllGeoGuesserGameSocketActions.LeavePlayer, ([action, hub]) =>
        hub.send(GpllGeoGuesserGameSocketCommands.ToServer.LeavePlayer, action),
    );

    private readonly startGame$ = this.handleAction(GpllGeoGuesserGameSocketActions.StartGame, ([action, hub]) =>
        hub.send(GpllGeoGuesserGameSocketCommands.ToServer.StartGame, action),
    );

    private readonly fetchGameContext$ = this.handleAction(
        GpllGeoGuesserGameSocketActions.FetchGameContext,
        ([action, hub]) => hub.send(GpllGeoGuesserGameSocketCommands.ToServer.FetchGameContext, action),
    );
}
