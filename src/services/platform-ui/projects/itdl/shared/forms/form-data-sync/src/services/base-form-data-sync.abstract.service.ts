import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { distinctUntilChanged, merge, of, pairwise, tap } from 'rxjs';

import { IFormDataSyncService } from '../models/form-data-sync-service.abstract.model';
import { IFormDataSyncControl } from '../models/interfaces/form-data-sync-control.interface';
import { IFormDataSyncSettings } from '../models/interfaces/form-data-sync-settings.interface';
import { DefaultSyncModel } from '../models/sync-models/default-sync.model';
import { SyncModelParams } from '../models/sync-models/sync-model.params';

export abstract class BaseFormDataSyncService implements IFormDataSyncService {
    constructor(protected readonly formDataSyncSettings: IFormDataSyncSettings) {}

    public filter(control: AbstractControl) {
        return this.isFormDataSyncControl(control);
    }

    public map(control: IFormDataSyncControl, model: any, form: FormGroup | FormArray) {
        const observable = merge(of(control.value), control.valueChanges).pipe(
            distinctUntilChanged(),
            pairwise(),
            tap(([previousValue, currentValue]) => this.sync(previousValue, currentValue, control, model, form)),
        );

        return observable;
    }

    protected sync(
        previousValue: any,
        currentValue: any,
        control: IFormDataSyncControl,
        model: any,
        form: FormGroup | FormArray,
    ) {
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        const formDataSyncModel = control.formDataSyncModel!;
        const params: SyncModelParams = {
            currentValue,
            previousValue,
            control,
            form,
            model,
        };

        if (typeof formDataSyncModel === 'string') {
            DefaultSyncModel.create({ modelPath: formDataSyncModel }).sync(params);
            return;
        }

        formDataSyncModel.sync(params);
    }

    private isFormDataSyncControl(control: IFormDataSyncControl): boolean {
        return !!control.formDataSyncModel;
    }
}
