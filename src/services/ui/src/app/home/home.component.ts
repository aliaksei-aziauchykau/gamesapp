import { Component, inject } from '@angular/core';
import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, map, of } from 'rxjs';
import { environment } from '../../environments/environment';

type StatusDto = {
  application: string;
  utcNow: string;
  environmentName: string;
};

@Component({
  selector: 'app-home',
  imports: [JsonPipe],
  template: `
    <section class="panel">
      <h2>GamesApp</h2>
      <p>Single-app Angular shell wired for Keycloak (OIDC) and the GamesApp API.</p>

      @if (status(); as s) {
        <pre class="code">{{ s | json }}</pre>
      } @else {
        <p class="muted">API status unavailable (is the API running?).</p>
      }

      <p class="muted">
        After Keycloak is configured, use <strong>Sign in</strong> and call <code>/api/games/profile</code> from the UI.
      </p>
    </section>
  `,
  styles: [
    `
      .panel {
        max-width: 920px;
        margin: 0 auto;
        border: 1px solid rgba(255, 255, 255, 0.1);
        border-radius: 14px;
        padding: 1rem 1.1rem;
        background: rgba(255, 255, 255, 0.03);
      }
      .code {
        overflow: auto;
        padding: 0.75rem;
        border-radius: 10px;
        background: rgba(0, 0, 0, 0.35);
        border: 1px solid rgba(255, 255, 255, 0.08);
      }
      .muted {
        opacity: 0.75;
        margin-top: 0.75rem;
      }
    `,
  ],
})
export class HomeComponent {
  private readonly http = inject(HttpClient);

  protected readonly status = toSignal(
    this.http.get<StatusDto>(`${environment.apiUrl}/api/games/status`).pipe(
      map((x) => x),
      catchError(() => of(null)),
    ),
    { initialValue: null as StatusDto | null },
  );
}
