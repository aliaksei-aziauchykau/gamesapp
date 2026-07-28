import { Injectable } from '@angular/core';
import { KrokoGameStoreActions } from '@itdl/game-portal/features/games/kroko-game/actions';
import {
    IKrokoGameContext,
    IKrokoGameSettings,
    IKrokoPlayerGameContext,
    KrokoGameContext,
    KrokoGameSettings,
    KrokoPlayerGameContext,
} from '@itdl/game-portal/features/games/kroko-game/entities';
import { IUserXSState, UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { Action, Selector, State, StateContext } from '@ngxs/store';

export interface IKrokoGameXSState {
    gameSettings: IKrokoGameSettings;
    gameContext: IKrokoGameContext;
    playerGameContext: IKrokoPlayerGameContext;
}

const defaults: IKrokoGameXSState = {
    gameSettings: new KrokoGameSettings(),
    gameContext: new KrokoGameContext(),
    playerGameContext: new KrokoPlayerGameContext(),
};

@State<IKrokoGameXSState>({
    name: 'krokoGameState',
    defaults,
})
@Injectable()
export class KrokoGameXSState {
    @Action(KrokoGameStoreActions.StoreGameContext)
    public storeGameContext(
        { patchState }: StateContext<IKrokoGameXSState>,
        { gameContext }: KrokoGameStoreActions.StoreGameContext,
    ) {
        patchState({ gameContext });
    }

    @Action(KrokoGameStoreActions.StorePlayerGameContext)
    public storePlayerGameContext(
        { patchState }: StateContext<IKrokoGameXSState>,
        { playerGameContext }: KrokoGameStoreActions.StorePlayerGameContext,
    ) {
        patchState({ playerGameContext });
    }

    @Action(KrokoGameStoreActions.CleanGameContext)
    public cleanGameContext({ dispatch }: StateContext<IKrokoGameXSState>) {
        return dispatch(new KrokoGameStoreActions.StoreGameContext(undefined));
    }

    @Action(KrokoGameStoreActions.StoreGameSettings)
    public storeGameSettings(
        { patchState }: StateContext<IKrokoGameXSState>,
        { gameSettings }: KrokoGameStoreActions.StoreGameSettings,
    ) {
        patchState({ gameSettings });
    }

    @Selector()
    public static gameContext(state: IKrokoGameXSState) {
        return state.gameContext;
    }

    @Selector()
    public static gameSettings(state: IKrokoGameXSState) {
        return state.gameSettings;
    }

    @Selector()
    public static players(state: IKrokoGameXSState) {
        return state.gameContext.players;
    }

    @Selector()
    public static currentRound(state: IKrokoGameXSState) {
        return state.gameContext.currentRound;
    }

    @Selector()
    public static currentRoundMessages(state: IKrokoGameXSState) {
        return state.gameContext.messages;
    }

    @Selector([KrokoGameXSState])
    public static currentRoundShapes(state: IKrokoGameXSState) {
        return state.gameContext.currentRound.shapesJson;
    }

    @Selector([KrokoGameXSState])
    public static currentRoundShapesParsed(state: IKrokoGameXSState) {
        return JSON.parse(state.gameContext.currentRound.shapesJson || '[]');
    }

    @Selector([KrokoGameXSState, UserXSState])
    public static isDrawer(state: IKrokoGameXSState, userState: IUserXSState) {
        return state.gameContext.currentRound.drawerUserId === userState.userId;
    }

    @Selector([KrokoGameXSState, UserXSState])
    public static isWinner(state: IKrokoGameXSState, userState: IUserXSState) {
        return (
            !!state.gameContext &&
            !!state.gameContext.currentRound &&
            !!state.gameContext.currentRound.winnerUserId &&
            state.gameContext.currentRound.winnerUserId === userState.userId
        );
    }

    @Selector()
    public static timer(state: IKrokoGameXSState) {
        return state.gameContext.timer;
    }

    @Selector()
    public static timerDate(state: IKrokoGameXSState) {
        return state.gameContext.timer;
    }
}
