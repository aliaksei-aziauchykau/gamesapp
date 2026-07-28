import { Injectable } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import {
    GeoGuesserGameContext,
    IGeoGuesserGameContext,
} from '@itdl/game-portal/features/games/geo-guesser-game/entities';
import { Action, Selector, State, StateContext } from '@ngxs/store';

import { GpllGeoGuesserStoreActions } from './geo-guesser-game.store.actions';

export interface IGeoGuesserGameXSState {
    gameContext: IGeoGuesserGameContext;
}

const defaults: IGeoGuesserGameXSState = {
    gameContext: new GeoGuesserGameContext(),
};

@State<IGeoGuesserGameXSState>({
    name: StorageKeys.GeoGuesserGameState,
    defaults,
})
@Injectable()
export class GpllGeoGuesserGameXSState {
    @Action(GpllGeoGuesserStoreActions.StoreGameContext)
    public storeGameContext(
        { patchState }: StateContext<IGeoGuesserGameXSState>,
        { gameContext }: GpllGeoGuesserStoreActions.StoreGameContext,
    ) {
        patchState({ gameContext });
    }

    @Action(GpllGeoGuesserStoreActions.ClearGameContext)
    public clearGameContext({ dispatch }: StateContext<IGeoGuesserGameXSState>) {
        return dispatch(new GpllGeoGuesserStoreActions.StoreGameContext(undefined));
    }

    @Selector()
    public static gameContext(state: IGeoGuesserGameXSState) {
        return state.gameContext;
    }

    @Selector()
    public static players(state: IGeoGuesserGameXSState) {
        return state.gameContext.players;
    }

    @Selector()
    public static currentRound(state: IGeoGuesserGameXSState) {
        return state.gameContext.currentRound;
    }

    @Selector()
    public static timer(state: IGeoGuesserGameXSState) {
        return state.gameContext.timer;
    }

    @Selector()
    public static timerDate(state: IGeoGuesserGameXSState) {
        return state.gameContext.timer;
    }
}
