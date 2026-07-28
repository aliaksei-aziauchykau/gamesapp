import { InjectionToken } from '@angular/core';

import { IFormDataSyncSettings } from '../models/interfaces/form-data-sync-settings.interface';

export const IFormDataSyncSettingsToken = new InjectionToken<IFormDataSyncSettings>('Abstraction for settings', {
    factory: () => {
        return {
            debounceTimeForSync: 0,
        };
    },
});
