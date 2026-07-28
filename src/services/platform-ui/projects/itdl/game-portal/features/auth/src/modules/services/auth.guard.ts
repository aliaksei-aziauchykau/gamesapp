import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';

export const GpllAuthGuardCanActivate = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const authGuard = inject(AuthGuard);
    return authGuard.canActivate(route, state);
};
