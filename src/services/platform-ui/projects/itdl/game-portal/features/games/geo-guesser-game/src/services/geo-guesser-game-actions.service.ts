import { inject, Injectable } from '@angular/core';
import { GpllGeoGuesserGameSocketActions } from '@itdl/game-portal/features/games/geo-guesser-game/socket';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';

@Injectable()
export class GpllGeoGuesserGameActionsService {
    private readonly uiActions = inject(ShrlUiActionsService);

    public getGameContextAction = (gameCode: string) =>
        this.uiActions.dispatch(new GpllGeoGuesserGameSocketActions.FetchGameContext(gameCode));

    public joinPlayerAction = (gameCode: string, userId: string, playerName: string) =>
        this.uiActions.dispatch(
            new GpllGeoGuesserGameSocketActions.JoinPlayer({ playerName, gameId: gameCode, userId }),
        );

    private leavePlayerAction = (gameId: string, userId: string) =>
        this.uiActions.dispatch(new GpllGeoGuesserGameSocketActions.LeavePlayer({ gameId, userId }));

    // Functions

    public getGameContextActionFn = (gameCode: string) => () => this.getGameContextAction(gameCode);

    public getJoinPlayerActionFn = (gameCode: string, userId: string) => (playerName: string) =>
        this.joinPlayerAction(gameCode, userId, playerName);

    public getLeavePlayerActionFn = (gameId: string, userId: string) => () => this.leavePlayerAction(gameId, userId);
}
