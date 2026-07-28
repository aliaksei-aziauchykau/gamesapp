import { inject, Injectable } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import { IPlayer, Player } from '@itdl/game-portal/features/player/entities';
import { Action, Selector, State, StateContext, Store } from '@ngxs/store';
import * as _ from 'lodash';

import { PlayerStoreActions } from './player.store.actions';

export interface IPlayerXSState {
    player: IPlayer;
}

const defaults: IPlayerXSState = {
    player: new Player(),
};

@State<IPlayerXSState>({
    name: StorageKeys.Player,
    defaults,
})
@Injectable()
export class PlayerXSState {
    private readonly store = inject(Store);

    @Action(PlayerStoreActions.GeneratePlayerNickname)
    public generatePlayerNickname({ dispatch, getState }: StateContext<IPlayerXSState>) {
        const player = getState().player;
        if (player.nickname) return;
        const playerNickname = _.uniqueId('Player_');
        return dispatch(new PlayerStoreActions.StorePlayerNickname(playerNickname));
    }

    @Action(PlayerStoreActions.StorePlayerNickname)
    public storePlayerNickname(
        { setState, getState }: StateContext<IPlayerXSState>,
        { playerNickname }: PlayerStoreActions.StorePlayerNickname,
    ) {
        const player = getState().player;
        setState({ player: { ...player, nickname: playerNickname } });
    }

    @Action(PlayerStoreActions.StorePlayer)
    public storePlayer({ patchState }: StateContext<IPlayerXSState>, { player }: PlayerStoreActions.StorePlayer) {
        patchState({ player });
    }

    @Selector()
    public static player(state: IPlayerXSState) {
        return state.player;
    }

    @Selector()
    public static playerNickname(state: IPlayerXSState) {
        return state.player.nickname;
    }
}
