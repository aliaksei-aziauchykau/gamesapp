import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

export abstract class IFormDataSyncService {
    public abstract filter(control: AbstractControl): boolean;
    public abstract map(control: AbstractControl, model: any, form: FormGroup | FormArray): Observable<any>;
}
