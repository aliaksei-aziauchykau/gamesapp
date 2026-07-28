import { CollectionNameType } from '@itdl/game-portal/ui/api-form/entities';

export namespace ApiTableStoreActions {
    export class FetchEntities {
        static readonly type = '[ApiTable] Fetch Entities';
        constructor(public collectionName: CollectionNameType) {}
    }

    export class StoreEntities {
        static readonly type = '[ApiTable] Store Entities';
        constructor(public collectionName: CollectionNameType, public entities: unknown[]) {}
    }
}
