import { InjectionToken } from '@angular/core';

export interface ISignalrConfig {
    apiUrl: string;
}

export const ShrlSignalrConfigToken = new InjectionToken<ISignalrConfig>('SignalrConfigToken');
