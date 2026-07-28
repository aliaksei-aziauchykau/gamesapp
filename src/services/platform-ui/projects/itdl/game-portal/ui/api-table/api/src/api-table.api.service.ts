import { inject, Injectable, Injector } from '@angular/core';
import { GplAppConfigProviderToken } from '@itdl/game-portal/common/config';
import { CollectionNameType } from '@itdl/game-portal/ui/api-form/entities';
import { ODataResponse, odataResponseValue, ServerSideDataEMPTY } from '@itdl/shared/common';
import { HttpService } from '@itdl/shared/http';
import { Observable } from 'rxjs';

@Injectable()
export class GpllApiTableApiService {
    private readonly httpService: HttpService = inject(HttpService);
    private readonly appConfig = inject(GplAppConfigProviderToken).config;
    readonly injector = inject(Injector);

    @ServerSideDataEMPTY()
    public getEntities<T>(collectionName: CollectionNameType): Observable<T[]> {
        return this.httpService
            .get<ODataResponse<T[]>>(`${this.appConfig.apiUrl}/api/v1/${collectionName}`)
            .pipe(odataResponseValue());
    }
}
