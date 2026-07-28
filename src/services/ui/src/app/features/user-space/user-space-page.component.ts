import { Component, inject } from '@angular/core';
import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, of } from 'rxjs';
import { API_BASE_URL } from '../../core/api.config';
import { SessionStore } from '../../session/session.store';

@Component({
  selector: 'app-user-space-page',
  imports: [JsonPipe],
  template: `
    <section class="panel">
      <h1>My space</h1>
      @if (session.status() !== 'authenticated') {
        <p class="muted">Sign in to view your profile.</p>
      } @else if (profile(); as p) {
        <pre class="code">{{ p | json }}</pre>
      } @else {
        <p class="muted">Profile unavailable.</p>
      }
    </section>
  `,
  styles: [
    `
      .panel {
        max-width: 720px;
        margin: 0 auto;
      }
      .code {
        overflow: auto;
        padding: 0.75rem;
        border-radius: 10px;
        background: rgba(0, 0, 0, 0.35);
      }
    `,
  ],
})
export class UserSpacePageComponent {
  protected readonly session = inject(SessionStore);
  private readonly http = inject(HttpClient);
  private readonly base = inject(API_BASE_URL);

  protected readonly profile = toSignal(
    this.http.get<unknown>(`${this.base}/api/games/profile`).pipe(catchError(() => of(null))),
    { initialValue: null },
  );
}
