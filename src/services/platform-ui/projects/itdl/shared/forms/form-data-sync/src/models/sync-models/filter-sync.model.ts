import { IFormDataSyncModel } from '../interfaces/form-data-sync-model.interface';
import { SyncModelParams } from './sync-model.params';

export class FilterSyncModel implements IFormDataSyncModel {
    syncModel!: IFormDataSyncModel;
    filter!: (params: SyncModelParams) => boolean;

    constructor(data: Partial<FilterSyncModel>) {
        Object.assign(this, data);
    }

    public static create(data: Partial<FilterSyncModel>) {
        return new FilterSyncModel(data);
    }

    public sync(params: SyncModelParams) {
        if (!this.filter(params)) return;

        this.syncModel.sync(params);
    }
}
