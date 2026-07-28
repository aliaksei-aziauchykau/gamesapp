import { isDevMode } from '@angular/core';
import { provideServiceWorker } from '@angular/service-worker';

export const serviceWorkerProviders = () => {
    return [
        provideServiceWorker('ngsw-worker.js', {
            enabled: !isDevMode(),
            registrationStrategy: 'registerWhenStable:30000',
        }),
    ];
};
