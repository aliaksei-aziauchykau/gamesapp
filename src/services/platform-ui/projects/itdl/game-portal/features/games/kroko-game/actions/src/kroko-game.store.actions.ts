/* eslint-disable sort-exports/sort-exports */
import {
    IKrokoGameContext,
    IKrokoGameSettings,
    IKrokoPlayerGameContext,
} from '@itdl/game-portal/features/games/kroko-game/entities';

export namespace KrokoGameStoreActions {
    // eslint-disable-next-line sort-exports/sort-exports
    export class CleanGameContext {
        static readonly type = '[Kroko Game] Clean Game Context';
    }

    export class RefreshGameContext {
        static readonly type = '[Kroko Game] Refresh Context';
        constructor(public gameId: string) {}
    }

    export class StoreGameContext {
        static readonly type = '[Kroko Game] Store Context';
        constructor(public gameContext: IKrokoGameContext | undefined) {}
    }

    export class StorePlayerGameContext {
        static readonly type = '[Kroko Game] Store Player Game Context';
        constructor(public playerGameContext: IKrokoPlayerGameContext) {}
    }

    export class StoreGameSettings {
        static readonly type = '[Kroko Game] Store Game Settings';
        constructor(public gameSettings: IKrokoGameSettings | undefined) {}
    }
}
