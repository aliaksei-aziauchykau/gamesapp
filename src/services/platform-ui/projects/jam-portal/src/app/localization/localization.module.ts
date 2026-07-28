import { APP_INITIALIZER, NgModule } from '@angular/core';
import { L10nIntlModule, L10nLoader, L10nTranslationModule } from 'angular-l10n';

import { appL10nInitializer } from './app-l10n-initializer.utils';
import { AppL10nTranslationLoader } from './app-l10n-translation-loader.service';
import { appL10nConfig } from './config/app-l10.config';

@NgModule({
    imports: [
        L10nTranslationModule.forRoot(appL10nConfig, {
            translationLoader: AppL10nTranslationLoader,
        }),
        L10nIntlModule,
    ],
    providers: [
        {
            provide: APP_INITIALIZER,
            useFactory: appL10nInitializer,
            deps: [L10nLoader],
            multi: true,
        },
    ],
})
export class GplLocalizationModule {}
