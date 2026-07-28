import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { provideOAuthClient } from 'angular-oauth2-oidc';
import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from '../environments/environment';

import { routes } from './app.routes';
import { SessionStore } from './session/session.store';
import { authInterceptor } from './core/auth.interceptor';

const authConfig: AuthConfig = {
  issuer: environment.keycloak.issuer,
  clientId: environment.keycloak.clientId,
  responseType: 'code',
  redirectUri: typeof window !== 'undefined' ? `${window.location.origin}/` : 'http://localhost:4200/',
  scope: 'openid profile email roles',
  strictDiscoveryDocumentValidation: false,
  showDebugInformation: !environment.production,
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideOAuthClient({
      resourceServer: {
        allowedUrls: [environment.apiUrl],
        sendAccessToken: true,
      },
    }),
    { provide: AuthConfig, useValue: authConfig },
    SessionStore,
  ],
};
