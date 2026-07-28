import { APP_INITIALIZER, ApplicationConfig, mergeApplicationConfig, TransferState } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideServerRendering } from '@angular/platform-server';
import { IServerData, serverDataStateKey, ServerDataToken } from '@itdl/game-portal/common';
import { GamePortalAppComponent } from './app/game-portal.app.component';
import { appConfig, IEnvironmentVars } from './app/game-portal.app.config';

const serverConfig: () => ApplicationConfig = () => ({
    providers: [provideServerRendering()],
});

const appConfigFn = (envVars: IEnvironmentVars, serverData: IServerData) =>
    appConfig(envVars, [
        {
            provide: APP_INITIALIZER,
            useFactory: (transferState) => () => transferState.set(serverDataStateKey, serverData),
            deps: [TransferState],
            multi: true,
        },
        {
            provide: ServerDataToken,
            useValue: serverData,
        },
    ]);

const bootstrap = () => {
    const envVars = getEnvVariablesWithPrefix('UI_') as IEnvironmentVars;
    const serverData: IServerData = { envVars } as unknown as IServerData;
    const appConfigWithEnvs = appConfigFn(envVars, serverData);
    const config = mergeApplicationConfig(appConfigWithEnvs, serverConfig());
    return bootstrapApplication(GamePortalAppComponent, config);
};

export default bootstrap;

function getEnvVariablesWithPrefix(prefix) {
    const envVars = {};
    for (const key in process.env) {
        if (key.startsWith(prefix)) {
            console.log('find key', key);
            const transformedKey = toCamelCase(key.slice(prefix.length));
            envVars[transformedKey] = process.env[key];
        }
    }
    return envVars;
}

function toCamelCase(str: string) {
    return str
        .replace(/_/g, ' ') // Replace underscores with spaces
        .split(' ') // Split the string into words
        .map((word, index) =>
            index === 0 ? word.toLowerCase() : word.charAt(0).toUpperCase() + word.slice(1).toLowerCase(),
        ) // Capitalize each word
        .join(''); // Join words back without spaces
}
