import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { GpllRolesEnum } from '@itdl/game-portal/features/auth/entities';
import { AuthXSState } from '@itdl/game-portal/features/auth/ng-xs';
import { Store } from '@ngxs/store';

export const getIsHasRoleFn = (role: GpllRolesEnum) => () => {
    const store = inject(Store);

    const isHasRole = store.selectSnapshot(AuthXSState.isHasRole(role));
    return isHasRole;
};

export const getIsHasRoleCanActivateFn: (role: GpllRolesEnum) => CanActivateFn =
    (role: GpllRolesEnum) => (_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) =>
        getIsHasRoleFn(role)();
