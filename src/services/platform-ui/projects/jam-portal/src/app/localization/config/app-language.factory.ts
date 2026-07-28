import { LanguageCode, LanguageType } from '@itdl/game-portal/localization';
import { ILanguageConfig } from '@itdl/shared/localization';

export class AppLanguageConfigFactory {
    public static languages: Record<LanguageType, ILanguageConfig> = {
        [LanguageType.English]: {
            type: LanguageType.English,
            code: LanguageCode.English,
            viewValue: 'English',
        },
        [LanguageType.Russian]: {
            type: LanguageType.Russian,
            code: LanguageCode.Russian,
            viewValue: 'Русский',
        },
        [LanguageType.Belarusian]: {
            type: LanguageType.Belarusian,
            code: LanguageCode.Belarusian,
            viewValue: 'Беларуская',
        },
    };

    public static languageSet: Record<LanguageType | LanguageCode, ILanguageConfig> = {
        [LanguageType.English]: AppLanguageConfigFactory.languages[LanguageType.English],
        [LanguageCode.English]: AppLanguageConfigFactory.languages[LanguageType.English],

        [LanguageType.Russian]: AppLanguageConfigFactory.languages[LanguageType.Russian],
        [LanguageCode.Russian]: AppLanguageConfigFactory.languages[LanguageType.Russian],

        [LanguageType.Belarusian]: AppLanguageConfigFactory.languages[LanguageType.Belarusian],
        [LanguageCode.Belarusian]: AppLanguageConfigFactory.languages[LanguageType.Belarusian],
    };

    public static get(languageType: LanguageType | LanguageCode) {
        return AppLanguageConfigFactory.languageSet[languageType];
    }
}
