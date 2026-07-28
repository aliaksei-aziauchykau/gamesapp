/* eslint-disable sort-exports/sort-exports */
import { inject, Injectable } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import { CollectionNameType } from '@itdl/game-portal/ui/api-form/entities';
import { GpllApiTableApiService } from '@itdl/game-portal/ui/api-table/api';
import { Dictionary } from '@itdl/shared/common';
import { Action, createSelector, State, StateContext } from '@ngxs/store';
import { tap } from 'rxjs';

import { ApiTableStoreActions } from './api-table.store.actions';

export interface IApiTableXSState {
    entities: Dictionary<CollectionNameType, unknown[]>;
}

const defaults: IApiTableXSState = {
    entities: {},
};

@State<IApiTableXSState>({
    name: StorageKeys.ApiTableState,
    defaults,
})
@Injectable()
export class ApiTableXSState {
    private readonly apiTableApiService = inject(GpllApiTableApiService);

    @Action(ApiTableStoreActions.FetchEntities)
    public fetchEntities(
        { dispatch }: StateContext<IApiTableXSState>,
        { collectionName }: ApiTableStoreActions.FetchEntities,
    ) {
        return this.apiTableApiService
            .getEntities(collectionName)
            .pipe(tap((entities) => dispatch(new ApiTableStoreActions.StoreEntities(collectionName, entities))));
    }

    @Action(ApiTableStoreActions.StoreEntities)
    public storeEntities(
        { patchState }: StateContext<IApiTableXSState>,
        { collectionName, entities }: ApiTableStoreActions.StoreEntities,
    ) {
        patchState({ entities: { ...{ [collectionName]: entities } } });
    }

    public static entities(collectionName: CollectionNameType) {
        return createSelector([ApiTableXSState], (state: IApiTableXSState) => {
            return state.entities[collectionName];
        });
    }
}
