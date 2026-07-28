import { AbstractControl } from '@angular/forms';

import { IFormDataSyncModel } from './form-data-sync-model.interface';

export interface IFormDataSyncControl extends AbstractControl {
    formDataSyncModel?: IFormDataSyncModel | string;
}
