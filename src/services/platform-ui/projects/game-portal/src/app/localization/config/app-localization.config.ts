import { LanguageType } from '@itdl/game-portal/localization';
import { ILocalizationFileConfig } from '@itdl/shared/localization';

import { AppLanguageConfigFactory } from './app-language.factory';

const a = AppLanguageConfigFactory.get(LanguageType.English);

export const appLocalizationFiles: ILocalizationFileConfig[] = [
    {
        prefix: 'assets/localization/locale',
        suffix: '.json',
    },
];
