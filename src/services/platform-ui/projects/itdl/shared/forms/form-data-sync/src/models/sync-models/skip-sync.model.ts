import { IFormDataSyncModel } from '../interfaces/form-data-sync-model.interface';
import { SyncModelParams } from './sync-model.params';

export class SkipSyncModel implements IFormDataSyncModel {
    syncModel!: IFormDataSyncModel;
    skip!: number;

    constructor(data: Partial<SkipSyncModel>) {
        Object.assign(this, data);
    }

    public static create(data: Partial<SkipSyncModel>) {
        return new SkipSyncModel(data);
    }

    public sync(params: SyncModelParams) {
        if (this.skip && this.skip--) return;

        this.syncModel.sync(params);
    }
}
