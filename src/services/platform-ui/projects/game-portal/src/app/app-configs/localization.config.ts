import { APP_INITIALIZER, importProvidersFrom } from '@angular/core';
import { L10nLoader, L10nTranslationModule } from 'angular-l10n';
import { appL10nInitializer } from '../localization/app-l10n-initializer.utils';
import { AppL10nTranslationLoader } from '../localization/app-l10n-translation-loader.service';
import { appL10nConfig } from '../localization/config/app-l10.config';

export const localizationsProviders = () => {
    return [
        importProvidersFrom(
            L10nTranslationModule.forRoot(appL10nConfig, {
                translationLoader: AppL10nTranslationLoader,
            }),
        ),
        {
            provide: APP_INITIALIZER,
            useFactory: appL10nInitializer,
            deps: [L10nLoader],
            multi: true,
        },
    ];
};
