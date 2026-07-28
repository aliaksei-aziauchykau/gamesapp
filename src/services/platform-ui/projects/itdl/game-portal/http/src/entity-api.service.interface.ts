import { Observable } from 'rxjs';
import { Entity } from './entity.abstract';

export interface IEntityApiService<TKey, TEntity extends Entity, TEntityToCreate = TEntity, TEntityToUpdate = TEntity> {
    getAllOData(oDataQuery: string): Observable<TEntity[]>;
    getAll(): Observable<TEntity[]>;
    getByIdOData(id: TKey, oDataQuery: string): Observable<TEntity>;
    getById(id: TKey): Observable<TEntity>;
    create(entity: TEntityToCreate): Observable<TEntity>;
    update(id: TKey, entity: TEntityToUpdate): Observable<TEntity>;
    delete(id: TKey): Observable<TEntity>;
}
