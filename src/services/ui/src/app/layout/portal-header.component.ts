import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { PreferencesStore } from '../core/preferences.store';
import { SessionStore } from '../session/session.store';

@Component({
  selector: 'app-portal-header',
  imports: [RouterLink, RouterLinkActive],
  template: `
    <header class="portal-header">
      <a routerLink="/" class="portal-header__brand">Games Portal</a>
      <nav class="portal-header__nav">
        <a routerLink="/" routerLinkActive="is-active" [routerLinkActiveOptions]="{ exact: true }">Home</a>
        <a routerLink="/gamedefs" routerLinkActive="is-active">Game definitions</a>
        <a routerLink="/game" routerLinkActive="is-active">Play</a>
        <a routerLink="/lobby" routerLinkActive="is-active">Lobby</a>
        <a routerLink="/space" routerLinkActive="is-active">My space</a>
        <a routerLink="/admin" routerLinkActive="is-active">Admin</a>
      </nav>
      <div class="portal-header__actions">
        <button type="button" class="btn btn--ghost" (click)="prefs.toggleTheme()">Theme</button>
        @if (session.status() === 'authenticated') {
          <span class="portal-header__user">{{ session.displayName() }}</span>
        } @else {
          <a routerLink="/login" class="btn">Sign in</a>
        }
      </div>
    </header>
  `,
  styles: [
    `
      .portal-header {
        display: flex;
        align-items: center;
        gap: 1rem;
        flex-wrap: wrap;
      }
      .portal-header__brand {
        font-weight: 650;
        color: inherit;
        text-decoration: none;
      }
      .portal-header__nav {
        display: flex;
        flex-wrap: wrap;
        gap: 0.85rem;
        flex: 1;
      }
      .portal-header__nav a {
        color: rgba(230, 237, 247, 0.85);
        text-decoration: none;
        font-size: 0.92rem;
      }
      .portal-header__nav a.is-active {
        color: #79b8ff;
        font-weight: 600;
      }
      .portal-header__actions {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }
      .portal-header__user {
        opacity: 0.85;
        font-size: 0.9rem;
      }
    `,
  ],
})
export class PortalHeaderComponent {
  protected readonly session = inject(SessionStore);
  protected readonly prefs = inject(PreferencesStore);
}
