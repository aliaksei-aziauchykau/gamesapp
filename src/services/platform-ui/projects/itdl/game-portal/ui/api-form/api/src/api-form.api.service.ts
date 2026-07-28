import { inject, Injectable, Injector } from '@angular/core';
import { GplAppConfigProviderToken } from '@itdl/game-portal/common/config';
import { CollectionNameType, IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';
import { ServerSideDataEMPTY } from '@itdl/shared/common';
import { HttpService } from '@itdl/shared/http';
import { Observable } from 'rxjs';
import { EntityApiServiceToken } from './api-form.api.tokens';

@Injectable()
export class GpllApiFormApiService {
    private readonly httpService: HttpService = inject(HttpService);
    private readonly appConfig = inject(GplAppConfigProviderToken).config;
    private readonly apiService = inject(EntityApiServiceToken);

    readonly injector = inject(Injector);

    @ServerSideDataEMPTY()
    public getUiSchema(collectionName: CollectionNameType): Observable<IFormEntitySchema> {
        return this.httpService.get<IFormEntitySchema>(`${this.appConfig.apiUrl}/api/v1/UiSchemas/GetUiSchema`, {
            params: { collectionSchemaName: collectionName },
        });
    }

    @ServerSideDataEMPTY()
    public create(entity: unknown): Observable<unknown> {
        return this.apiService.create(entity);
    }

    @ServerSideDataEMPTY()
    public update(entity: unknown & { id: unknown }): Observable<unknown> {
        return this.apiService.update(entity.id, entity);
    }

    @ServerSideDataEMPTY()
    public delete(id: unknown): Observable<unknown> {
        return this.apiService.delete(id);
    }
}
