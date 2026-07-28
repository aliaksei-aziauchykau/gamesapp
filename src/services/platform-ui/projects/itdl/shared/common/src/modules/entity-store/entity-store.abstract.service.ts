import { EntityToStore } from './entity-to-store.type';

export abstract class EntityStoreService<T> {
    private readonly entities: Record<string, T> = {};

    public registerEntity(entityToStore: EntityToStore<T>) {
        this.entities[entityToStore.key] = entityToStore.entity;
    }

    public unregisterEntity(key: string) {
        delete this.entities[key];
    }

    public getEntity(key: string) {
        return this.entities[key];
    }

    public getEntities() {
        return Object.values(this.entities);
    }
}
