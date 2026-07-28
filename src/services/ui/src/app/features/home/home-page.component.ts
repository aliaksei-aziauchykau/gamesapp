import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, of } from 'rxjs';
import { GamesApiService } from '../../core/games-api.service';
import { GameDefsStore } from '../game-defs/game-defs.store';

@Component({
  selector: 'app-home-page',
  imports: [RouterLink],
  providers: [GameDefsStore],
  template: `
    <section class="panel">
      <h1>Welcome to Games Portal</h1>
      <p class="lead">
        Migrated from <code>volnyja-siervisy/ui-apps/projects/game-portal</code> as one Angular 20 application
        with feature folders and <code>@ngrx/signals</code> stores (no Nx sub-libraries).
      </p>

      @if (status(); as s) {
        <p class="muted">API: {{ s.application }} · {{ s.environmentName }}</p>
      }

      <h2>Featured games</h2>
      @if (defs.loading()) {
        <p class="muted">Loading game definitions…</p>
      } @else if (defs.error(); as err) {
        <p class="muted">Could not load definitions: {{ err }}</p>
      } @else {
        <div class="cards">
          @for (g of defs.items(); track g.id) {
            <article class="card">
              @if (g.imageUrl) {
                <img [src]="g.imageUrl" [alt]="g.imageAlt ?? g.name" />
              }
              <h3>{{ g.name }}</h3>
              <p>{{ g.description }}</p>
              <a class="card__link" [routerLink]="['/gamedefs']" [queryParams]="{ code: g.code }">Details</a>
              @if (g.gamePageAppUrl) {
                <a class="card__link" [routerLink]="['/game']" [queryParams]="{ code: g.code }">Play</a>
              }
            </article>
          }
        </div>
      }

      <p class="muted">
        Configure Keycloak realm <code>gamesapp</code> and run DB migration
        <code>002_GamePortalSchema.sql</code> for full data.
      </p>
    </section>
  `,
  styles: [
    `
      .panel {
        max-width: 1100px;
        margin: 0 auto;
      }
      .lead {
        line-height: 1.45;
        opacity: 0.92;
      }
      .cards {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(240px, 1fr));
        gap: 1rem;
        margin: 1rem 0;
      }
      .card {
        border: 1px solid rgba(255, 255, 255, 0.1);
        border-radius: 12px;
        padding: 1rem;
        background: rgba(255, 255, 255, 0.03);
      }
      .card img {
        max-width: 100%;
        height: 120px;
        object-fit: contain;
        margin-bottom: 0.5rem;
      }
      .card__link {
        display: inline-block;
        margin-right: 0.75rem;
        margin-top: 0.5rem;
        color: #79b8ff;
      }
      .muted {
        opacity: 0.75;
      }
    `,
  ],
})
export class HomePageComponent implements OnInit {
  private readonly api = inject(GamesApiService);
  protected readonly defs = inject(GameDefsStore);

  protected readonly status = toSignal(this.api.getStatus().pipe(catchError(() => of(null))), {
    initialValue: null,
  });

  ngOnInit(): void {
    this.defs.load();
  }
}
