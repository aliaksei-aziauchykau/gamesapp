import { bootstrapApplication } from '@angular/platform-browser';
import { IClientData } from '@itdl/game-portal/common';

import { JamPortalAppComponent } from './app/jam-portal.app.component';
import { appConfig, IEnvironmentVars } from './app/jam-portal.app.config';

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

    return bootstrapApplication(
        JamPortalAppComponent,
        appConfig(clientData.envVars as unknown as IEnvironmentVars),
    ).catch((err) => console.error(err));
};

bootstrap();
