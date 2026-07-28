import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import { PreferencesStore } from '../core/preferences.store';
import { SessionStore } from '../session/session.store';
import { PortalFooterComponent } from './portal-footer.component';
import { PortalHeaderComponent } from './portal-header.component';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, PortalHeaderComponent, PortalFooterComponent],
  template: `
    <div class="portal-layout">
      <app-portal-header />
      @if (session.status() === 'authenticated') {
        <div class="portal-layout__auth">
          <button type="button" class="btn btn--ghost" (click)="logout()">Sign out</button>
        </div>
      }
      <main class="portal-layout__main">
        <router-outlet />
      </main>
      <app-portal-footer />
    </div>
  `,
  styles: [
    `
      .portal-layout {
        min-height: 100vh;
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
      }
      .portal-layout__auth {
        display: flex;
        justify-content: flex-end;
        padding: 0 1.25rem;
      }
      .portal-layout__main {
        flex: 1;
        padding: 0 1.25rem 1.25rem;
      }
    `,
  ],
})
export class MainLayoutComponent implements OnInit {
  protected readonly session = inject(SessionStore);
  private readonly oauth = inject(OAuthService);
  private readonly authConfig = inject(AuthConfig);
  private readonly prefs = inject(PreferencesStore);

  ngOnInit(): void {
    this.prefs.setTheme('dark');
    this.oauth.configure(this.authConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin().then(() => this.syncSession());
    this.oauth.events.subscribe(() => this.syncSession());
  }

  logout(): void {
    this.oauth.revokeTokenAndLogout();
    this.session.setSession('anonymous', null);
  }

  private syncSession(): void {
    if (this.oauth.hasValidAccessToken()) {
      const claims = this.oauth.getIdentityClaims() as Record<string, unknown> | null;
      const name =
        (claims?.['preferred_username'] as string | undefined) ??
        (claims?.['name'] as string | undefined) ??
        null;
      this.session.setSession('authenticated', name);
      return;
    }
    this.session.setSession('anonymous', null);
  }
}
