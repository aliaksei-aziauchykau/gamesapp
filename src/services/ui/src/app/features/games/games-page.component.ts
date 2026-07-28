import { Component, inject, OnInit } from '@angular/core';
import { JsonPipe } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, of } from 'rxjs';
import { GamesApiService } from '../../core/games-api.service';

@Component({
  selector: 'app-games-page',
  imports: [JsonPipe],
  template: `
    <section class="panel">
      <h1>Games</h1>
      <p class="lead">Active sessions (migrated from <code>/api/v1/Games</code>).</p>
      @if (sessions(); as rows) {
        @if (rows.length === 0) {
          <p class="muted">No sessions yet. Run database migrator after seed schema.</p>
        } @else {
          <pre class="code">{{ rows | json }}</pre>
        }
      } @else {
        <p class="muted">Could not load sessions.</p>
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
export class GamesPageComponent implements OnInit {
  private readonly api = inject(GamesApiService);
  protected readonly sessions = toSignal(
    this.api.getGameSessions().pipe(catchError(() => of(null))),
    { initialValue: null },
  );

  ngOnInit(): void {}
}
