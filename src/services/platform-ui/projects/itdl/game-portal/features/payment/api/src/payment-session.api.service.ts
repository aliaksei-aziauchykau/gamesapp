import { Injectable } from '@angular/core';
import { ApiService } from '@itdl/game-portal/http/api';
import { ODataResponse } from '@itdl/shared/common';
import { Observable } from 'rxjs';

@Injectable()
export class GpllPaymentSessionApiService extends ApiService {
    protected override getEndpointUrl(): string {
        return '/api/v1/PaymentSessions';
    }

    public createCheckoutSession(): Observable<ODataResponse<string>> {
        return this.httpService.post<ODataResponse<string>, unknown>(`${this.baseUrl}/CreateCheckoutSession`);
    }
}
