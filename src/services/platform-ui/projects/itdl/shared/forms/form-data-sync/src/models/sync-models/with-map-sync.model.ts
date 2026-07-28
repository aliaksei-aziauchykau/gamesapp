import * as _ from 'lodash';

import { IFormDataSyncModel } from '../interfaces/form-data-sync-model.interface';
import { SyncModelParams } from './sync-model.params';

export class WithMapSyncModel implements IFormDataSyncModel {
    modelPath!: string;
    handler!: (params: SyncModelParams) => any;

    constructor(data: Partial<WithMapSyncModel>) {
        Object.assign(this, data);
    }

    public static create(data: Partial<WithMapSyncModel>) {
        return new WithMapSyncModel(data);
    }

    public sync(params: SyncModelParams) {
        const { model, currentValue, previousValue, form, control } = params;
        _.set(
            model,
            this.modelPath,
            this.handler({
                previousValue,
                currentValue,
                form,
                model,
                control,
            }),
        );
    }
}
