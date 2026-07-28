import { AsyncValidatorFn, FormControl, FormControlState, ValidatorFn } from '@angular/forms';
import { IFormDataSyncControl, IFormDataSyncModel } from '@itdl/shared/forms/form-data-sync';

export interface IGpllFormControlOptions<TValue = any> {
    formState?: FormControlState<TValue> | TValue;
    validator?: ValidatorFn | ValidatorFn[] | null;
    asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[] | null;
    formDataSyncModel?: IFormDataSyncModel | string;
}

// eslint-disable-next-line sort-exports/sort-exports
export class GpllFormControl<TValue = any> extends FormControl implements IFormDataSyncControl {
    public formDataSyncModel?: IFormDataSyncModel | string;

    constructor(
        formState?: FormControlState<TValue> | TValue,
        validator?: ValidatorFn | ValidatorFn[] | null,
        asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[] | null,
        formDataSyncModel?: IFormDataSyncModel | string,
    ) {
        super(formState, validator, asyncValidator);
        this.formDataSyncModel = formDataSyncModel;
    }

    public static create<TValue>(options?: IGpllFormControlOptions<TValue>) {
        return new GpllFormControl(
            options?.formState,
            options?.validator,
            options?.asyncValidator,
            options?.formDataSyncModel,
        );
    }
}
