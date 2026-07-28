import { SyncModelParams } from '../sync-models/sync-model.params';

export interface IFormDataSyncModel {
    sync(params: SyncModelParams): void;
}
