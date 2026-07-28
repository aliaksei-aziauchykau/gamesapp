import { inject, InjectionToken } from '@angular/core';
import { HttpService } from '@itdl/shared/http';

export const API_URL = new InjectionToken<string>('Api Url');

export abstract class ApiService {
    protected httpService = inject(HttpService);
    protected odataApiUrl = inject(API_URL);

    protected abstract getEndpointUrl(): string;
    protected baseUrl!: string;

    // the token gets injected into the service
    constructor() {
        this.baseUrl = `${this.odataApiUrl}${this.getEndpointUrl()}`;
    }
}
