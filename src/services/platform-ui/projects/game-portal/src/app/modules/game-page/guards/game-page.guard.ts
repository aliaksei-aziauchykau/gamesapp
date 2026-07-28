import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';
import { GameDefinitionActions } from '@itdl/game-portal/features/game-definition/ng-xs';
import { GameActions, GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { Store } from '@ngxs/store';
import { catchError, map, of, switchMap, withLatestFrom } from 'rxjs';

export const IsGameCodeExistCanActivate: CanActivateFn = (route: ActivatedRouteSnapshot) => {
    const store = inject(Store);
    const gameCode = route.params['gameCode'];
    const router = inject(Router);

    return store.dispatch(new GameActions.FetchGamesByGameCode(gameCode)).pipe(
        withLatestFrom(store.select(GameXSState.games)),
        map(([_, games]) => {
            if (games.length === 0) throw new Error('Game not found');

            const game = games[0];
            store.dispatch(new GameActions.StoreGame(game));
            return game;
        }),
        switchMap((game) => {
            return store.dispatch(new GameDefinitionActions.FetchById(game.gameDefinitionId));
        }),
        map(() => true),
        catchError(() => of(router.parseUrl('/gamedefs'))),
    );
};
