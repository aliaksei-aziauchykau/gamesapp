import { LanguageCode } from '@itdl/game-portal/localization';
import { L10nConfig } from 'angular-l10n';

export const appL10nConfig: L10nConfig = {
    format: 'language-region',
    providers: [{ name: 'app', asset: [] }],
    fallback: false,
    cache: false,
    keySeparator: '.',
    defaultLocale: { language: LanguageCode.Belarusian },
    schema: [
        {
            locale: { language: LanguageCode.English },
            dir: 'ltr',
        },
        {
            locale: { language: LanguageCode.Belarusian },
            dir: 'ltr',
        },
        {
            locale: { language: LanguageCode.Russian },
            dir: 'ltr',
        },
    ],
};
