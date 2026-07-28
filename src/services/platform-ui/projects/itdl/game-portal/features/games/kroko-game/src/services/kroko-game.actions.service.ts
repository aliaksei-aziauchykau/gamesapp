import { inject, Injectable } from '@angular/core';
import { KrokoGameSocketActions } from '@itdl/game-portal/features/games/kroko-game/actions';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';

@Injectable()
export class GpllKrokoGameActionsService {
    private readonly uiActions = inject(ShrlUiActionsService);

    public getGameContextAction = (gameCode: string) =>
        this.uiActions.dispatch(new KrokoGameSocketActions.FetchGameContext(gameCode));

    public getGameSettingsAction = (gameCode: string) =>
        this.uiActions.dispatch(new KrokoGameSocketActions.FetchGameSettings(gameCode));

    public joinPlayerAction = (gameCode: string, userId: string, playerName: string) =>
        this.uiActions.dispatch(new KrokoGameSocketActions.JoinPlayer({ playerName, gameId: gameCode, userId }));

    private leavePlayerAction = (gameId: string, userId: string) =>
        this.uiActions.dispatch(new KrokoGameSocketActions.LeavePlayer({ gameId, userId }));

    private drawWordAction = (gameId: string, userId: string, shapesJson: string) =>
        this.uiActions.dispatch(new KrokoGameSocketActions.DrawWord({ gameId, userId, shapesJson }));

    private guessWordAction = (gameId: string, userId: string, message: string) =>
        this.uiActions.dispatch(new KrokoGameSocketActions.GuessWord({ gameId, userId, message }));

    private storeSelectedWordAction = (gameId: string, userId: string, wordSelected: string) =>
        this.uiActions.dispatch(new KrokoGameSocketActions.StoreSelectedWord({ gameId, userId, wordSelected }));

    // Functions

    public getGameContextActionFn = (gameCode: string) => () => this.getGameContextAction(gameCode);

    public getGameSettingsActionFn = (gameCode: string) => () => this.getGameSettingsAction(gameCode);

    public getJoinPlayerActionFn = (gameCode: string, userId: string) => (playerName: string) =>
        this.joinPlayerAction(gameCode, userId, playerName);

    public getLeavePlayerActionFn = (gameId: string, userId: string) => () => this.leavePlayerAction(gameId, userId);

    public getDrawWordActionFn = (gameCode: string, userId: string) => (shapeJson: string) =>
        this.drawWordAction(gameCode, userId, shapeJson);

    public getGuessWordActionFn = (gameId: string, userId: string) => (message: string) =>
        this.guessWordAction(gameId, userId, message);

    public getStoreSelectedWordActionFn = (gameCode: string, userId: string) => (wordSelected: string) =>
        this.storeSelectedWordAction(gameCode, userId, wordSelected);
}
