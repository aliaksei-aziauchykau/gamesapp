import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { getIsHasRoleFn } from '@itdl/game-portal/common/guards/src/has-role.guard';
import { GpllRolesEnum } from '@itdl/game-portal/features/auth/entities';

export const IsAdminPageCanActivate: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const isHasRole = getIsHasRoleFn(GpllRolesEnum.Admin)();
    const router = inject(Router);

    if (!isHasRole) return router.parseUrl('/admin/error');

    return true;
};
