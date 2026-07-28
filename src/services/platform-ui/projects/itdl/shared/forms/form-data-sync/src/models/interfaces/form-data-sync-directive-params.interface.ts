import { FormArray, FormGroup } from '@angular/forms';

import { IFormDataSyncService } from '../form-data-sync-service.abstract.model';

export interface IFormDataSyncDirectiveParams {
    form: FormGroup | FormArray;
    model: any;
    service?: IFormDataSyncService;
}
