/* eslint-disable sort-exports/sort-exports */
import { inject, Injectable } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import { GpllApiFormApiService } from '@itdl/game-portal/ui/api-form/api';
import { CollectionNameType, IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';
import { Dictionary } from '@itdl/shared/common';
import { Action, createSelector, Selector, State, StateContext, Store } from '@ngxs/store';
import { tap } from 'rxjs';

import { ApiFormStoreActions } from './api-form.store.actions';

export interface IApiFormXSState {
    schemas: Dictionary<CollectionNameType, IFormEntitySchema>;
}

const defaults: IApiFormXSState = {
    schemas: {},
};

@State<IApiFormXSState>({
    name: StorageKeys.ApiFormState,
    defaults,
})
@Injectable()
export class ApiFormXSState {
    private readonly apiFormApiService = inject(GpllApiFormApiService);
    private readonly store = inject(Store);

    @Action(ApiFormStoreActions.FetchUiSchema)
    public fetchUiSchema(
        { dispatch }: StateContext<IApiFormXSState>,
        { collectionName }: ApiFormStoreActions.FetchUiSchema,
    ) {
        return this.apiFormApiService
            .getUiSchema(collectionName)
            .pipe(tap((uiSchema) => dispatch(new ApiFormStoreActions.StoreUiSchema(collectionName, uiSchema))));
    }

    @Action(ApiFormStoreActions.StoreUiSchema)
    public storeUiSchema(
        { patchState }: StateContext<IApiFormXSState>,
        { collectionName, uiSchema }: ApiFormStoreActions.StoreUiSchema,
    ) {
        patchState({ schemas: { ...{ [collectionName]: uiSchema } } });
    }

    @Action(ApiFormStoreActions.UpdateEntity, { cancelUncompleted: true })
    public updateEntity({ patchState }: StateContext<IApiFormXSState>, { entity }: ApiFormStoreActions.UpdateEntity) {
        return this.apiFormApiService.update(entity);
    }

    @Selector()
    public static schemas(state: IApiFormXSState) {
        return state.schemas;
    }

    public static uiSchema(collectionName: CollectionNameType) {
        return createSelector([ApiFormXSState], (state: IApiFormXSState) => {
            return state.schemas[collectionName];
        });
    }
}
