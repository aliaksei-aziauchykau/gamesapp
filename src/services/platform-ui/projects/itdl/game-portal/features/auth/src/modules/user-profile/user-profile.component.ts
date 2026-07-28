import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AuthService, IdToken } from '@auth0/auth0-angular';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { RxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';

type State = {
    idToken: IdToken;
};

@Component({
    selector: 'gpll-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.scss'],
    imports: [CommonModule, RxLet],
    providers: [RxState],
    standalone: true,
})
export class GpllUserProfileComponent {
    private readonly authService = inject(AuthService);

    public readonly idToken$ = this.state.select('idToken');

    constructor(private readonly state: RxState<State>) {
        this.state.connect('idToken', this.authService.idTokenClaims$.pipe(isNotNullOrUndefined()));
    }
}
