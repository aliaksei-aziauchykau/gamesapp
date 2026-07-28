import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { LanguageCode } from '@itdl/game-portal/localization';
import { L10N_LOCALE, L10nLocale, L10nTranslationService } from 'angular-l10n';
import isArray from 'lodash-es/isArray';

@Injectable()
export class GpllTranslateService {
    public readonly translationChange$ = this.translationService.onChange();
    constructor(
        @Inject(L10N_LOCALE) public locale: L10nLocale,
        @Inject(DOCUMENT) public document: Document,
        private readonly translationService: L10nTranslationService,
    ) {}

    public translate(keys: string | string[], params?: unknown, language: string = this.locale.language): string {
        const translation = this.translationService.translate(keys, params, language);
        return typeof translation === 'string'
            ? translation
            : isArray(keys)
            ? (keys as string[]).join(' ')
            : (keys as string);
    }

    public setLocaleLanguage(languageCode: LanguageCode) {
        this.translationService.setLocale({ language: languageCode });
        this.document.documentElement.lang = languageCode;
    }

    public getLocale(): L10nLocale {
        return this.translationService.getLocale();
    }
}
