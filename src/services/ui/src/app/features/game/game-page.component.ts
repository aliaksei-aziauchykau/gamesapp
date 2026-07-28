import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, map, of, switchMap } from 'rxjs';
import { GamesApiService } from '../../core/games-api.service';

@Component({
  selector: 'app-game-page',
  template: `
    <section class="panel">
      <h1>Play</h1>
      @if (definition(); as def) {
        <p>Game: <strong>{{ def.name }}</strong> ({{ def.code }})</p>
        <p class="muted">
          Game canvas (Phaser / Kroko / GeoGuesser) will be ported into this feature module. Route slug:
          <code>{{ def.gamePageAppUrl }}</code>
        </p>
      } @else {
        <p class="muted">Pick a game from <a href="/gamedefs">game definitions</a> or pass <code>?code=</code>.</p>
      }
    </section>
  `,
  styles: [
    `
      .panel {
        max-width: 720px;
        margin: 0 auto;
      }
      a {
        color: #79b8ff;
      }
    `,
  ],
})
export class GamePageComponent {
  private readonly route = inject(ActivatedRoute);
  private readonly api = inject(GamesApiService);

  protected readonly definition = toSignal(
    this.route.queryParamMap.pipe(
      map((p) => p.get('code')),
      switchMap((code) =>
        code ? this.api.getGameDefinition(code).pipe(catchError(() => of(null))) : of(null),
      ),
    ),
    { initialValue: null },
  );
}
