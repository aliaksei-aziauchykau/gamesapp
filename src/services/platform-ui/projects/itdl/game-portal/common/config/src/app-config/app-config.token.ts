import { InjectionToken } from '@angular/core';

import { IGplAppConfigProvider } from './app-config.provider.interface';

export const GplAppConfigProviderToken = new InjectionToken<IGplAppConfigProvider>('Gpl token config provider');
