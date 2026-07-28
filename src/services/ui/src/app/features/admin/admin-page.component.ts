import { Component, inject } from '@angular/core';
import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, of } from 'rxjs';
import { API_BASE_URL } from '../../core/api.config';

@Component({
  selector: 'app-admin-page',
  imports: [JsonPipe],
  template: `
    <section class="panel">
      <h1>Admin</h1>
      <p class="lead">Platform module manifest and future admin tools (legacy <code>admin</code> route).</p>
      @if (modules(); as m) {
        <pre class="code">{{ m | json }}</pre>
      } @else {
        <p class="muted">Module manifest unavailable.</p>
      }
    </section>
  `,
  styles: [
    `
      .panel {
        max-width: 920px;
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
export class AdminPageComponent {
  private readonly http = inject(HttpClient);
  private readonly base = inject(API_BASE_URL);

  protected readonly modules = toSignal(
    this.http
      .get<{ application: string; modules: unknown[] }>(`${this.base}/api/platform/modules`)
      .pipe(catchError(() => of(null))),
    { initialValue: null },
  );
}
