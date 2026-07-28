import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';

export namespace GameDefinitionActions {
    export class FetchByName {
        static readonly type = '[Game Definition] Fetch Current Game Definition By Name ';
        constructor(public gameDefinitionName: string) {}
    }

    export class FetchById {
        static readonly type = '[Game Definition] Fetch Current Game Definition By Id ';
        constructor(public gameDefinitionId: number) {}
    }

    export class FetchAll {
        static readonly type = '[Game Definition] Fetch Game Definitions';
    }

    export class StoreCurrent {
        static readonly type = '[Game Definition] Store Current Game Definitions';
        constructor(public gameDefinition: GameDefinition) {}
    }

    export class StoreAll {
        static readonly type = '[Game Definition] Store Game Definitions';
        constructor(public gameDefinitions: GameDefinition[]) {}
    }
}
