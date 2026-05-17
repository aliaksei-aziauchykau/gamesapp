import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import { SessionStore } from './session/session.store';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  protected readonly session = inject(SessionStore);
  private readonly oauth = inject(OAuthService);
  private readonly authConfig = inject(AuthConfig);

  ngOnInit(): void {
    this.oauth.configure(this.authConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin().then(() => {
      if (this.oauth.hasValidAccessToken()) {
        const claims = this.oauth.getIdentityClaims() as Record<string, unknown> | null;
        const name =
          (claims?.['preferred_username'] as string | undefined) ??
          (claims?.['name'] as string | undefined) ??
          null;
        this.session.setSession('authenticated', name);
      } else {
        this.session.setSession('anonymous', null);
      }
    });
  }

  login(): void {
    this.oauth.initLoginFlow();
  }

  logout(): void {
    this.oauth.revokeTokenAndLogout();
  }
}
