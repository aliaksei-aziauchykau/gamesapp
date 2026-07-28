import { inject } from '@angular/core';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { pipe, switchMap, tap } from 'rxjs';
import { GamesApiService } from '../../core/games-api.service';
import { GameDefinition } from '../../shared/models/game-definition.model';

type GameDefsState = {
  items: GameDefinition[];
  loading: boolean;
  error: string | null;
};

const initial: GameDefsState = { items: [], loading: false, error: null };

export const GameDefsStore = signalStore(
  withState(initial),
  withMethods((store, api = inject(GamesApiService)) => ({
    load: rxMethod<void>(
      pipe(
        tap(() => patchState(store, { loading: true, error: null })),
        switchMap(() =>
          api.getGameDefinitions().pipe(
            tap({
              next: (items) => patchState(store, { items, loading: false }),
              error: (err: Error) =>
                patchState(store, { loading: false, error: err.message ?? 'Failed to load' }),
            }),
          ),
        ),
      ),
    ),
  })),
);
