import { ApplicationConfig, EnvironmentProviders, Provider, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { localizationsProviders } from './app-configs/localization.config';
import { ngcProviders } from './app-configs/ngc.config';
import { ngXsStoreProviders } from './app-configs/ngxs-store.config';
import { serviceWorkerProviders } from './app-configs/service-worker.config';
import { stripeProviders } from './app-configs/stripe.config';
import { routes } from './jam-portal.app.routes';

export interface IEnvironmentVars {
    apiUrl: string;
    authDomain: string;
    authClientId: string;
    authAudience: string;
    authScope: string;
    stripePublicKey: string;
}

export const appConfig: (
    envVars: IEnvironmentVars,
    additionalProviders?: (Provider | EnvironmentProviders)[],
) => ApplicationConfig = (envVars, additionalProviders = []) => {
    console.log('envs bo', envVars);

    return {
        providers: [
            provideZoneChangeDetection({ eventCoalescing: true }),
            provideRouter(routes),
            provideClientHydration(),
            provideHttpClient(withFetch()),
            provideAnimations(),
            ...serviceWorkerProviders(),
            ...ngXsStoreProviders(),
            ...ngcProviders(),
            ...localizationsProviders(),
            ...stripeProviders(envVars),
            ...additionalProviders,
        ],
    };
};
