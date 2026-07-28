import { CommonModule } from '@angular/common';
import { Component, ContentChild, inject, TemplateRef } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { GpllIdToken } from '@itdl/game-portal/features/auth/entities';
import { AuthStoreActions } from '@itdl/game-portal/features/auth/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { map, tap } from 'rxjs';

@Component({
    selector: 'gpll-auth-context',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './auth-context.component.html',
    styleUrls: ['./auth-context.component.scss'],
    providers: [RxState],
})
export class GpllAuthContextComponent {
    @ContentChild(TemplateRef) content!: TemplateRef<unknown>;

    private readonly store = inject(Store);
    private readonly authService = inject(AuthService);

    private readonly isAuthenticatedEffect$ = this.authService.isAuthenticated$.pipe(
        tap((isAuthenticated) => this.store.dispatch(new AuthStoreActions.StoreIsAuthenticated(isAuthenticated))),
    );

    private readonly idTokenEffect$ = this.authService.idTokenClaims$.pipe(
        isNotNullOrUndefined(),
        map((idToken) => new GpllIdToken(idToken)),
        tap((idToken) => this.store.dispatch(new AuthStoreActions.StoreIdToken(idToken))),
    );

    private readonly userEffect$ = this.authService.user$.pipe(
        isNotNullOrUndefined(),
        tap((user) => this.store.dispatch(new AuthStoreActions.StoreUser(user))),
    );

    constructor(private readonly state: RxState<object>) {
        this.state.hold(this.isAuthenticatedEffect$);
        this.state.hold(this.idTokenEffect$);
        this.state.hold(this.userEffect$);
    }
}
