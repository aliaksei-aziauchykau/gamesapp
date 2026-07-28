import { ApplicationConfig, EnvironmentProviders, Provider, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { ServerDataToken } from '@itdl/game-portal/common';
import { GplAppConfigProviderToken, IGplAppConfig, IGplAppConfigProvider } from '@itdl/game-portal/common/config';
import { API_URL } from '@itdl/game-portal/http/api';
import { getWindowOutsideAngular } from '@itdl/shared/common';
import { auth0Providers } from './app-configs/auth0.config';
import { localizationsProviders } from './app-configs/localization.config';
import { ngcProviders } from './app-configs/ngc.config';
import { ngXsStoreProviders } from './app-configs/ngxs-store.config';
import { serviceWorkerProviders } from './app-configs/service-worker.config';
import { stripeProviders } from './app-configs/stripe.config';
import { routes } from './game-portal.app.routes';

class GplAppConfigProvider implements IGplAppConfigProvider {
    constructor(public readonly config: IGplAppConfig) {}
}

const windowObject = getWindowOutsideAngular();

export interface IEnvironmentVars {
    baseHref: string;
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
            ...auth0Providers(envVars, windowObject),
            ...ngXsStoreProviders(),
            ...ngcProviders(),
            ...localizationsProviders(),
            ...stripeProviders(envVars),
            { provide: API_URL, useValue: envVars.apiUrl },
            {
                provide: GplAppConfigProviderToken,
                useValue: new GplAppConfigProvider({
                    apiUrl: envVars.apiUrl,
                }),
            },
            { provide: ServerDataToken, useValue: {} },
            ...additionalProviders,
        ],
    };
};
