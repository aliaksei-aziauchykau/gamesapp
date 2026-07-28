import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthConfig, AuthHttpInterceptor, provideAuth0 } from '@auth0/auth0-angular';
import { IEnvironmentVars } from '../game-portal.app.config';

export const auth0Providers = (envVars: IEnvironmentVars, windowObject: any) => {
    const config: AuthConfig = {
        domain: envVars.authDomain,
        clientId: envVars.authClientId,
        authorizationParams: {
            redirect_uri: `${windowObject?.location?.origin}`,
            audience: envVars.authAudience,
            scope: envVars.authScope,
        },
        httpInterceptor: {
            allowedList: [
                // {
                //     uri: `${environment.odataApiUrl}/api/v1/Games*`,
                //     allowAnonymous: true,
                // },
                // {
                //     uri: `${environment.odataApiUrl}/api/v1/GameDefinitions*`,
                //     allowAnonymous: true,
                // },
                // {
                //     uri: `${environment.odataApiUrl}/api/v1/Games*`,
                //     httpMethod: 'GET',
                //     tokenOptions: {
                //         authorizationParams: {
                //             scope: 'openid profile email read:games',
                //         },
                //     },
                // },
            ],
        },
    };

    return [
        provideAuth0(config),
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthHttpInterceptor,
            multi: true,
        },
    ];
};
