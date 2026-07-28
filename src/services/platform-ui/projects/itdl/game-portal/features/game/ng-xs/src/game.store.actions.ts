import { Game } from '@itdl/game-portal/features/game/entities';
import { IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

export namespace GameActions {
    export class FetchGames {
        static readonly type = '[Game] Fetch Games';
        constructor(public gameDefinitionName: string) {}
    }

    export class FetchGamesByGameDefinitionId {
        static readonly type = '[Game] Fetch Games by Game Definition Id';
        constructor(public gameDefinitionId: number) {}
    }

    export class FetchGamesByGameCode {
        static readonly type = '[Game] Fetch Games by Game Cod';
        constructor(public gameCode: string) {}
    }

    export class FetchUiSchema {
        static readonly type = '[Game] Fetch UI Schema';
    }

    export class StoreUiSchema {
        static readonly type = '[Game] Store UI Schema';
        constructor(public uiSchema: IFormEntitySchema) {}
    }

    export class CreateGame {
        static readonly type = '[Game] Create Game';
        constructor(public gameDefinitionId: number, public name: string) {}
    }

    export class StoreGames {
        static readonly type = '[Game] Store Games';
        constructor(public games: Game[]) {}
    }

    export class StoreCreatedGame {
        static readonly type = '[Game] Store Created Games';
        constructor(public game: Game) {}
    }

    export class StoreGame {
        static readonly type = '[Game] Store Game';
        constructor(public game: Game | null) {}
    }
}
