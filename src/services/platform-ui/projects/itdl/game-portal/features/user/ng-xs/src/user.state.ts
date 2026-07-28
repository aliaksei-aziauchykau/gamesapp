import { inject, Injectable } from '@angular/core';
import { CryptToken } from '@itdl/shared/common';
import { Action, Selector, State, StateContext, Store } from '@ngxs/store';

import { UserActions } from './user.actions';

export interface IUserXSState {
    userId: string | null;
}

const defaults: IUserXSState = {
    userId: null,
};

@State<IUserXSState>({
    name: 'user',
    defaults,
})
@Injectable()
export class UserXSState {
    private readonly store = inject(Store);
    private readonly cryptToken = inject(CryptToken);

    @Action(UserActions.GenerateUserId)
    public generateUserId({ dispatch }: StateContext<IUserXSState>) {
        const currentUserId = this.store.selectSnapshot(UserXSState.userId);
        const userId = currentUserId ?? this.cryptToken.randomUUID();

        return dispatch(new UserActions.StoreUserId(userId));
    }

    @Action(UserActions.StoreUserId)
    public storeGameContext({ patchState }: StateContext<IUserXSState>, { userId }: UserActions.StoreUserId) {
        patchState({ userId });
    }

    @Selector()
    public static userId(state: IUserXSState) {
        return state.userId;
    }

    @Selector()
    public static userIdSafety(state: IUserXSState) {
        if (state.userId === null) throw new Error('User id is not set');
        return state.userId;
    }
}
