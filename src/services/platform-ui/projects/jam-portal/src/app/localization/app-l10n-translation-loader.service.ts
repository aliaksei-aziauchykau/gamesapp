import { isPlatformServer } from '@angular/common';
import { HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { CacheObservable } from '@itdl/shared/common';
import { HttpService } from '@itdl/shared/http';
import { ILocalizationFileConfig } from '@itdl/shared/localization';
import { L10nProvider, L10nTranslationLoader } from 'angular-l10n';
import merge from 'lodash-es/merge';
import { forkJoin, from, map, Observable } from 'rxjs';

import { appLocalizationFiles } from './config/app-localization.config';

type JsonFileType = { [key: string]: unknown };

@Injectable()
export class AppL10nTranslationLoader implements L10nTranslationLoader {
    private readonly headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    // eslint-disable-next-line @typescript-eslint/ban-types
    constructor(@Inject(PLATFORM_ID) private readonly platformId: Object, private readonly httpService: HttpService) {}

    @CacheObservable()
    public get(languageCode: string, _provider: L10nProvider): Observable<JsonFileType> {
        const options = {
            headers: this.headers,
        };

        const isServer = isPlatformServer(this.platformId);

        // Нн забудь кеш

        // if (isServer) {
        //     const a = import('../../assets/localization/locale-by.json');
        //     a.then((data) => console.log('aaa', data));
        //     console.log('bbb', a);

        //     return from(a);
        // }

        // : from(import(`../../${url}`) as Promise<JsonFileType>

        const requests = appLocalizationFiles
            .map((file) => ({ file, languageCode, url: this.buildUrl(languageCode, file) }))
            .map(({ file, url, languageCode }) =>
                isServer ? this.getJsonAsImport(file, languageCode) : this.httpService.get<JsonFileType>(url, options),
            );

        return forkJoin(requests).pipe(map((data) => this.mergeLocalizationFiles(data)));
    }

    private getJsonAsImport(file: ILocalizationFileConfig, languageCode: string) {
        const promise = import(
            /* webpackMode: "eager" */
            `../../${file.prefix}/${languageCode}.json`
        );

        return from(promise);
    }

    // private ssr(languageCode: string) {
    //     const requests = appLocalizationFiles
    //         .map((file) => this.buildUrl(languageCode, file))
    //         .map((url) => from(import(url) as Promise<JsonFileType>));

    //     return forkJoin(requests).pipe(map((data) => this.mergeLocalizationFiles(data)));
    // }

    private buildUrl(language: string, file: ILocalizationFileConfig) {
        const url = `/${file.prefix}/${language}${file.suffix}`;

        return url;
    }

    private mergeLocalizationFiles(data: JsonFileType[]) {
        return data.reduce((previousFile, currentFile) => merge(previousFile, currentFile));
    }
}
