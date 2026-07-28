import { Inject, Injectable } from '@angular/core';

import { IFormDataSyncSettings } from '../models/interfaces/form-data-sync-settings.interface';
import { IFormDataSyncSettingsToken } from '../tokens/form-data-sync-settings.token';
import { BaseFormDataSyncService } from './base-form-data-sync.abstract.service';

@Injectable()
export class ShrlDefaultFormDataSyncService extends BaseFormDataSyncService {
    constructor(@Inject(IFormDataSyncSettingsToken) formDataSyncSettings: IFormDataSyncSettings) {
        super(formDataSyncSettings);
    }
}
