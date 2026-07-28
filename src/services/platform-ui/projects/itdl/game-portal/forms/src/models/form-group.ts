import { AsyncValidatorFn, FormControl, FormGroup, ValidatorFn } from '@angular/forms';
import { IFormDataSyncControl, IFormDataSyncModel } from '@itdl/shared/forms/form-data-sync';

export interface IGpllFormGroupOptions {
    controls?: { [key: string]: FormControl };
    validator?: ValidatorFn | ValidatorFn[] | null;
    asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[] | null;
    formDataSyncModel?: IFormDataSyncModel | string;
}

export class GpllFormGroup extends FormGroup implements IFormDataSyncControl {
    public formDataSyncModel?: IFormDataSyncModel | string;

    constructor(
        controls?: { [key: string]: FormControl },
        validator?: ValidatorFn | ValidatorFn[] | null,
        asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[] | null,
        formDataSyncModel?: IFormDataSyncModel | string,
    ) {
        super(controls, validator, asyncValidator);
        this.formDataSyncModel = formDataSyncModel;
    }

    public static create(options: IGpllFormGroupOptions) {
        return new GpllFormGroup(
            options.controls,
            options.validator,
            options.asyncValidator,
            options.formDataSyncModel,
        );
    }
}
