/* eslint-disable sort-exports/sort-exports */
import { Injectable } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import { State } from '@ngxs/store';

export interface IProjectItemContainerXSState {}

const defaults: IProjectItemContainerXSState = {};

@State<IProjectItemContainerXSState>({
    name: StorageKeys.ProjectItemContainerState,
    defaults,
})
@Injectable()
export class ProjectItemContainerXSState {
    // @Action(ProjectItemContainerStoreActions.FetchUiSchema)
    // public example(
    //     { dispatch }: StateContext<IProjectItemContainerXSState>,
    //     { collectionName }: ProjectItemContainerStoreActions.FetchUiSchema,
    // ) {
    //
    //     return this.ProjectItemContainerApiService
    //         .getUiSchema(collectionName)
    //         .pipe(tap((uiSchema) => dispatch(new ProjectItemContainerStoreActions.StoreUiSchema(collectionName, uiSchema))));
    // }
    // @Selector()
    // public static schemas(state: IProjectItemContainerXSState) {
    //     return state.schemas;
    // }
    // public static uiSchema(collectionName: CollectionNameType) {
    //     return createSelector([ProjectItemContainerXSState], (state: IProjectItemContainerXSState) => {
    //         return state.schemas[collectionName];
    //     });
    // }
}
