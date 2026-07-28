import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { SessionStore } from '../../session/session.store';

@Component({
  selector: 'app-login-page',
  template: `
    <section class="panel">
      <h1>Sign in</h1>
      <p class="lead">Keycloak OIDC (replaces Auth0 from the legacy game-portal stack).</p>
      <button type="button" class="btn" (click)="login()">Continue with Keycloak</button>
      @if (session.status() === 'authenticated') {
        <p class="muted">You are already signed in.</p>
      }
    </section>
  `,
  styles: [
    `
      .panel {
        max-width: 480px;
        margin: 2rem auto;
        text-align: center;
      }
    `,
  ],
})
export class LoginPageComponent {
  private readonly oauth = inject(OAuthService);
  private readonly router = inject(Router);
  protected readonly session = inject(SessionStore);

  login(): void {
    this.oauth.initLoginFlow();
  }
}
