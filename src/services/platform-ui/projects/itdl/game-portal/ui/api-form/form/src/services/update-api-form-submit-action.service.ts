// import { inject, Injectable } from '@angular/core';
// import { ActionQueueStoreActions } from '@itdl/game-portal/features/action-queue';
// import { ApiFormStoreActions } from '@itdl/game-portal/ui/api-form/ng-xs';
// import { Store } from '@ngxs/store';

// import { CollectionNameType } from '@itdl/game-portal/ui/api-form/entities';
// import { IApiFormSubmitActionService } from '../models/api-form-submit-action.service.interface';

// @Injectable({ providedIn: 'root' })
// export class DefaultApiFormSubmitActionService implements IApiFormSubmitActionService {
//     private readonly store = inject(Store);

//     public submit(collectionName: CollectionNameType, entity: unknown): void {
//         const action = new ApiFormStoreActions.UpdateEntity(collectionName, entity as any);
//         this.store.dispatch(new ActionQueueStoreActions.AddToQueue(action));
//     }
// }
