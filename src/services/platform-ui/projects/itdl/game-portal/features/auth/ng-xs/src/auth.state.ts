/* eslint-disable sort-exports/sort-exports */
import { Injectable } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import { GpllPermissionsEnum, GpllRolesEnum, GpllUser, IGpllIdToken } from '@itdl/game-portal/features/auth/entities';
import { Action, createSelector, Selector, State, StateContext } from '@ngxs/store';

import { AuthStoreActions } from './auth.actions';

export interface IAuthXSState {
    isAuthenticated: boolean | null;
    idToken: IGpllIdToken | null;
    user: GpllUser | null;
    roles: Record<string, string>;
    permissions: Record<string, string>;
}

const defaults: IAuthXSState = {
    isAuthenticated: false,
    idToken: null,
    user: null,
    roles: {},
    permissions: {},
};

@State<IAuthXSState>({
    name: StorageKeys.Auth,
    defaults,
})
@Injectable()
export class AuthXSState {
    @Action(AuthStoreActions.StoreIsAuthenticated)
    public storeIsAuthenticated(
        { patchState }: StateContext<IAuthXSState>,
        { isAuthenticated }: AuthStoreActions.StoreIsAuthenticated,
    ) {
        patchState({ isAuthenticated });
    }

    @Action(AuthStoreActions.StoreIdToken)
    public storeIdToken({ patchState }: StateContext<IAuthXSState>, { idToken }: AuthStoreActions.StoreIdToken) {
        const permissions = idToken.scopes
            ? idToken.scopes.split(' ').reduce((acc, permission) => ({ ...acc, [permission]: permission }), {})
            : {};

        const roles = idToken.cus_roles ? idToken.cus_roles.reduce((acc, role) => ({ ...acc, [role]: role }), {}) : {};

        patchState({ idToken });
        patchState({ roles });
        patchState({ permissions });
    }

    @Action(AuthStoreActions.StoreUser)
    public storeUser({ patchState }: StateContext<IAuthXSState>, { user }: AuthStoreActions.StoreUser) {
        patchState({ user });
    }

    @Selector()
    public static isAuthenticated(state: IAuthXSState) {
        return state.isAuthenticated;
    }

    @Selector()
    public static idToken(state: IAuthXSState) {
        return state.idToken;
    }

    public static isHasRole(role: GpllRolesEnum) {
        return createSelector([AuthXSState], (state: IAuthXSState) => {
            return !!state.isAuthenticated && !!state.roles[role];
        });
    }

    public static isHasPermission(permission: GpllPermissionsEnum) {
        return createSelector([AuthXSState], (state: IAuthXSState) => {
            return !!state.isAuthenticated && !!state.permissions[permission];
        });
    }
}
