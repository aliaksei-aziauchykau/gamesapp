import { IFormDataSyncModel } from '../interfaces/form-data-sync-model.interface';
import { SyncModelParams } from './sync-model.params';

export class CustomSyncModel implements IFormDataSyncModel {
    handler!: (params: SyncModelParams) => void;

    constructor(data: Partial<CustomSyncModel>) {
        Object.assign(this, data);
    }

    public static create(data: Partial<CustomSyncModel>) {
        return new CustomSyncModel(data);
    }

    public sync(params: SyncModelParams) {
        const { model, currentValue, previousValue, form, control } = params;
        this.handler({
            previousValue,
            currentValue,
            form,
            model,
            control,
        });
    }
}
