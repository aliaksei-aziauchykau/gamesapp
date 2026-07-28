import { APP_BASE_HREF } from '@angular/common';
import { bootstrapApplication } from '@angular/platform-browser';
import { IClientData } from '@itdl/game-portal/common';
import { GamePortalAppComponent } from './app/game-portal.app.component';
import { appConfig, IEnvironmentVars } from './app/game-portal.app.config';

export function getBaseHref(): string {
    // Custom logic to determine base URL
    return document.getElementsByTagName('base')[0]?.getAttribute('href') || '/';
}

const bootstrap = () => {
    // return fetch(`${window.location.origin}/assets/app.config.json`)
    //     .then((response) => response.json())
    //     .then((rootConfig) => bootstrapApplication(GamePortalAppComponent, appConfig(rootConfig)))
    //     .catch((err) => console.error(err));

    const selector = "script[id='ng-state']";
    const scriptData = document.querySelector<HTMLElement>(selector);
    if (!scriptData) throw new Error(`Script with selector ${selector} not found`);
    const transferData = JSON.parse(scriptData.innerText);
    const clientData = transferData?.dataFromServer as IClientData;
    console.log('aaaaa', getBaseHref());
    return bootstrapApplication(
        GamePortalAppComponent,
        appConfig(clientData.envVars as unknown as IEnvironmentVars, [
            { provide: APP_BASE_HREF, useFactory: getBaseHref },
        ]),
    ).catch((err) => console.error(err));
};

bootstrap();
