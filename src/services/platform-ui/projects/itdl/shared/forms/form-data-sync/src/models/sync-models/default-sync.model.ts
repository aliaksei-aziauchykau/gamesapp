import { set } from 'lodash-es';

import { IFormDataSyncModel } from '../interfaces/form-data-sync-model.interface';
import { SyncModelParams } from './sync-model.params';

export class DefaultSyncModel implements IFormDataSyncModel {
    modelPath!: string;

    constructor(data: Partial<DefaultSyncModel>) {
        Object.assign(this, data);
    }

    public static create(data: Partial<DefaultSyncModel>) {
        return new DefaultSyncModel(data);
    }

    public sync(params: SyncModelParams) {
        const { model, currentValue } = params;
        set(model, this.modelPath, currentValue);
    }
}
