import { AbstractControl, FormArray, FormGroup } from '@angular/forms';

export interface SyncModelParams {
    currentValue: any;
    previousValue: any;
    model: any;
    form: FormGroup | FormArray;
    control: AbstractControl;
}
