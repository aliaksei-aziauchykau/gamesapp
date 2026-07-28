import { Component, inject } from '@angular/core';
import { JsonPipe } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, of } from 'rxjs';
import { GamesApiService } from '../../core/games-api.service';

@Component({
  selector: 'app-lobby-page',
  imports: [JsonPipe],
  template: `
    <section class="panel">
      <h1>Lobby</h1>
      <p class="lead">Open lobbies from the Games API.</p>
      @if (lobbies(); as rows) {
        <pre class="code">{{ rows | json }}</pre>
      } @else {
        <p class="muted">No lobby data available.</p>
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
export class LobbyPageComponent {
  private readonly api = inject(GamesApiService);
  protected readonly lobbies = toSignal(this.api.getLobbies().pipe(catchError(() => of(null))), {
    initialValue: null,
  });
}
