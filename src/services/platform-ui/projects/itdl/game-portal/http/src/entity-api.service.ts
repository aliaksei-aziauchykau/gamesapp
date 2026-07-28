import { DOCUMENT } from '@angular/common';
import { inject, Injector } from '@angular/core';
import { GplAppConfigProviderToken } from '@itdl/game-portal/common/config';
import { IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';
import {
    IInjectorProvider,
    ODataResponse,
    odataResponseValue,
    ServerSideDataEMPTY,
    ServerSideDataEmptyArray,
} from '@itdl/shared/common';
import { HttpService } from '@itdl/shared/http';
import { Observable } from 'rxjs';

import { IEntityApiService } from './entity-api.service.interface';
import { Entity } from './entity.abstract';
import { ODataQuery } from './query/query';

export abstract class EntityApiService<
    TKey,
    TEntity extends Entity,
    TEntityToCreate = TEntity,
    TEntityToUpdate = TEntity,
> implements IEntityApiService<TKey, TEntity, TEntityToCreate, TEntityToUpdate>, IInjectorProvider
{
    protected abstract getApiUrl(): string;
    protected apiUrl!: string;

    readonly injector = inject(Injector);
    private readonly window = inject(DOCUMENT).defaultView;
    private readonly appConfig = inject(GplAppConfigProviderToken).config;
    private readonly httpService: HttpService = inject(HttpService);

    private get collectionName() {
        return this.getApiUrl().split('/').pop();
    }

    constructor() {
        if (!this.window) throw new Error('Window is not defined!');
        this.apiUrl = `${this.appConfig.apiUrl}${this.getApiUrl()}`;
    }

    @ServerSideDataEmptyArray()
    public getAllOData(oDataQuery: string): Observable<TEntity[]> {
        return this.httpService
            .get<ODataResponse<TEntity[]>>(`${this.apiUrl}/${ODataQuery.create(oDataQuery).query}`)
            .pipe(odataResponseValue());
    }

    @ServerSideDataEmptyArray()
    public getAll(): Observable<TEntity[]> {
        return this.httpService.get<ODataResponse<TEntity[]>>(this.apiUrl).pipe(odataResponseValue());
    }

    @ServerSideDataEMPTY()
    public getByIdOData(id: TKey, oDataQuery: string): Observable<TEntity> {
        return this.httpService.get<TEntity>(`${this.apiUrl}/${id}${ODataQuery.create(oDataQuery).query}`);
    }

    @ServerSideDataEMPTY()
    public getById(id: TKey): Observable<TEntity> {
        return this.httpService.get<TEntity>(`${this.apiUrl}/${id}`);
    }

    @ServerSideDataEMPTY()
    public getUiSchema(): Observable<IFormEntitySchema> {
        return this.httpService.get<IFormEntitySchema>(
            `${this.appConfig.apiUrl}/UiSchema/GetUiSchema/${this.collectionName}`,
        );
    }

    @ServerSideDataEMPTY()
    public create(entity: TEntityToCreate): Observable<TEntity> {
        return this.httpService.post<TEntity, TEntityToCreate>(this.apiUrl, entity);
    }

    @ServerSideDataEMPTY()
    public update(id: TKey, entity: TEntityToUpdate): Observable<TEntity> {
        return this.httpService.put<TEntity, TEntityToUpdate>(`${this.apiUrl}/${id}`, entity);
    }

    @ServerSideDataEMPTY()
    public delete(id: TKey): Observable<TEntity> {
        return this.httpService.delete<TEntity>(`${this.apiUrl}/${id}`);
    }
}
