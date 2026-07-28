import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { HttpOptions } from '../models/types/http-options.type';
import { HttpQueryParams } from '../models/types/http-query-params.type';
import { EncodeHttpParamCodec } from '../utils/encode-http-param.codec';

@Injectable({ providedIn: 'root' })
export class HttpService {
    private readonly httpOptionsDefault: HttpOptions = { withCredentials: true };

    constructor(private readonly httpClient: HttpClient) {}

    public get<T>(url: string, options: HttpOptions = {}): Observable<T> {
        const mergedOptions = { ...this.httpOptionsDefault, ...options } as HttpOptions;
        mergedOptions.params = this.getQueryParams(mergedOptions.params);
        return this.httpClient
            .get<T>(url, { ...mergedOptions })
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    public post<T, K>(url: string, data?: K, options: HttpOptions = {}): Observable<T> {
        const mergedOptions = { ...this.httpOptionsDefault, ...options };
        mergedOptions.params = this.getQueryParams(mergedOptions.params);

        return this.httpClient
            .post<T>(url, data, { ...mergedOptions })
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    public put<T, K>(url: string, data: K, options: HttpOptions = {}): Observable<T> {
        const mergedOptions = { ...this.httpOptionsDefault, ...options };
        mergedOptions.params = this.getQueryParams(mergedOptions.params);

        return this.httpClient
            .put<T>(url, data, { ...mergedOptions })
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    public delete<T>(url: string, options: HttpOptions = {}): Observable<T> {
        const mergedOptions = { ...this.httpOptionsDefault, ...options };
        mergedOptions.params = this.getQueryParams(mergedOptions.params);

        return this.httpClient
            .delete<T>(url, { ...mergedOptions })
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    private handleError(err: HttpErrorResponse) {
        if (err.error instanceof ErrorEvent) {
            console.error('An error occurred:', err.error.message);
        } else {
            // eslint-disable-next-line no-console
            console.error(`Backend returned code ${err.status}, body was: ${err.error}`);
        }

        return throwError(() => err);
    }

    private cleanObj(obj: any) {
        if (!obj) return obj;

        for (const propName in obj) {
            if (obj[propName] === null || obj[propName] === undefined) {
                delete obj[propName];
            }
        }

        return obj;
    }

    private getQueryParams(queryParams: HttpQueryParams | undefined) {
        const params = new HttpParams({
            encoder: new EncodeHttpParamCodec(),
            fromObject: this.cleanObj(queryParams),
        });
        return params;
    }
}
