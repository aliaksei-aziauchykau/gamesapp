import { CollectionNameType, IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

export namespace ApiFormStoreActions {
    export class FetchUiSchema {
        static readonly type = '[ApiForm] Fetch Ui Schema';
        constructor(public collectionName: CollectionNameType) {}
    }

    export class StoreUiSchema {
        static readonly type = '[Game] Fetch Games by Game Definition Id';
        constructor(public collectionName: CollectionNameType, public uiSchema: IFormEntitySchema) {}
    }

    export class CreateEntity {
        static readonly type = '[ApiForm] Create Entity';
        constructor(public entity: unknown) {}
    }

    export class UpdateEntity {
        static readonly type = '[ApiForm] Update Entity';
        constructor(public entity: unknown & { id: unknown }) {}
    }
}
