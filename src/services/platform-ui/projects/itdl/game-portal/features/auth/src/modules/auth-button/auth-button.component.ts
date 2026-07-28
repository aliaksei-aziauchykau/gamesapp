import { CommonModule, DOCUMENT } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '@auth0/auth0-angular';
import { RxLet } from '@rx-angular/template/let';

@Component({
    selector: 'gpll-auth-button',
    standalone: true,
    imports: [CommonModule, RxLet, MatButtonModule],
    templateUrl: './auth-button.component.html',
    styleUrls: ['./auth-button.component.scss'],
})
export class GpllAuthButtonComponent {
    public document = inject(DOCUMENT);

    constructor(public readonly authService: AuthService) {}

    public loginWithRedirect(): void {
        // this.authService.getAccessTokenSilently({
        //     authorizationParams: {

        //     }
        // })
        this.authService.loginWithRedirect({ appState: { target: `${this.document.location.origin}/gameDefs` } });
    }

    public logout(): void {
        this.authService.logout({ logoutParams: { returnTo: this.document.location.origin } });
    }
}
