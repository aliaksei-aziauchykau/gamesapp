import * as _ from 'lodash';

import { IFormDataSyncModel } from '../interfaces/form-data-sync-model.interface';
import { SyncModelParams } from './sync-model.params';

export class TrimValueSyncModel implements IFormDataSyncModel {
    modelPath!: string;

    constructor(data: Partial<TrimValueSyncModel>) {
        Object.assign(this, data);
    }

    public static create(data: Partial<TrimValueSyncModel>) {
        return new TrimValueSyncModel(data);
    }

    public sync(params: SyncModelParams) {
        const { model, currentValue } = params;
        _.set(model, this.modelPath, currentValue?.trim());
    }
}
