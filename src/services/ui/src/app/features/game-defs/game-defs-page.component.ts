import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { GameDefsStore } from './game-defs.store';

@Component({
  selector: 'app-game-defs-page',
  imports: [RouterLink],
  providers: [GameDefsStore],
  template: `
    <section class="panel">
      <h1>Game definitions</h1>
      <p class="lead">Catalog migrated from BelGamePortal <code>GameDefinition</code> entity.</p>

      @if (store.loading()) {
        <p class="muted">Loading…</p>
      } @else if (store.error(); as err) {
        <p class="muted">{{ err }}</p>
      } @else {
        <ul class="list">
          @for (g of store.items(); track g.id) {
            <li [class.is-selected]="highlight() === g.code">
              <strong>{{ g.name }}</strong>
              <span class="code">{{ g.code }}</span>
              <p>{{ g.description }}</p>
              <a [routerLink]="['/game']" [queryParams]="{ code: g.code }">Open game</a>
            </li>
          }
        </ul>
      }
    </section>
  `,
  styles: [
    `
      .panel {
        max-width: 920px;
        margin: 0 auto;
      }
      .list {
        list-style: none;
        padding: 0;
        display: grid;
        gap: 1rem;
      }
      .list li {
        border: 1px solid rgba(255, 255, 255, 0.1);
        border-radius: 12px;
        padding: 1rem;
      }
      .list li.is-selected {
        border-color: #79b8ff;
      }
      .code {
        font-family: ui-monospace, monospace;
        font-size: 0.85rem;
        opacity: 0.8;
        margin-left: 0.5rem;
      }
      a {
        color: #79b8ff;
      }
    `,
  ],
})
export class GameDefsPageComponent implements OnInit {
  protected readonly store = inject(GameDefsStore);
  private readonly route = inject(ActivatedRoute);

  protected readonly highlight = () => this.route.snapshot.queryParamMap.get('code');

  ngOnInit(): void {
    this.store.load();
  }
}
