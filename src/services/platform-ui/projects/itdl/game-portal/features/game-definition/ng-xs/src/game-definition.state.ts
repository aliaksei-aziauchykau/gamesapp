/* eslint-disable sort-exports/sort-exports */
import { inject, Injectable } from '@angular/core';
import { GpllGameDefinitionApiService } from '@itdl/game-portal/features/game-definition/api';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import buildQuery from 'odata-query';
import { filter, tap } from 'rxjs';

import { GameDefinitionActions } from './game-definition.actions';

export interface IGameDefinitionXSState {
    current: GameDefinition | null;
    gameDefinitions: GameDefinition[];
}

const defaults: IGameDefinitionXSState = {
    current: null,
    gameDefinitions: [],
};

@State<IGameDefinitionXSState>({
    name: 'gameDefinitionsState',
    defaults,
})
@Injectable()
export class GameDefinitionXSState {
    private readonly gameDefinitionApiService = inject(GpllGameDefinitionApiService);

    @Action(GameDefinitionActions.FetchAll)
    public fetchAllGameDefinitions({ dispatch }: StateContext<IGameDefinitionXSState>) {
        return this.gameDefinitionApiService
            .getAll()
            .pipe(tap((games) => dispatch(new GameDefinitionActions.StoreAll(games))));
    }

    @Action(GameDefinitionActions.StoreAll)
    public storeAllGameDefinitions(
        { patchState }: StateContext<IGameDefinitionXSState>,
        { gameDefinitions }: GameDefinitionActions.StoreAll,
    ) {
        patchState({ gameDefinitions: [...(gameDefinitions || [])] });
    }

    @Action(GameDefinitionActions.StoreCurrent)
    public storeCurrentGameDefinition(
        { patchState }: StateContext<IGameDefinitionXSState>,
        { gameDefinition }: GameDefinitionActions.StoreCurrent,
    ) {
        patchState({ current: gameDefinition });
    }

    @Action(GameDefinitionActions.FetchByName)
    public fetchByNameByNameGameDefinition(
        { dispatch }: StateContext<IGameDefinitionXSState>,
        { gameDefinitionName }: GameDefinitionActions.FetchByName,
    ) {
        return this.gameDefinitionApiService
            .getAllOData(buildQuery({ filter: { name: gameDefinitionName }, expand: 'games' }))
            .pipe(
                filter((x) => x.length > 0),
                tap((games) => dispatch(new GameDefinitionActions.StoreCurrent(games[0]))),
            );
    }

    @Action(GameDefinitionActions.FetchById)
    public fetchByIdGameDefinition(
        { dispatch }: StateContext<IGameDefinitionXSState>,
        { gameDefinitionId }: GameDefinitionActions.FetchById,
    ) {
        return this.gameDefinitionApiService
            .getById(gameDefinitionId)
            .pipe(tap((gameDefinition) => dispatch(new GameDefinitionActions.StoreCurrent(gameDefinition))));
    }

    @Selector()
    public static gameDefinitions(state: IGameDefinitionXSState) {
        return state.gameDefinitions;
    }

    @Selector()
    public static currentGameDefinition(state: IGameDefinitionXSState) {
        return state.current;
    }
}
