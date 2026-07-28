import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { provideServerRendering } from '@angular/platform-server';
import { appConfig } from './vs-landing.app.config';

const serverConfig: ApplicationConfig = {
    providers: [provideServerRendering()],
};

export const config = mergeApplicationConfig(appConfig, serverConfig);
