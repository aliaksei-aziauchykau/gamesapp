/* eslint-disable sort-exports/sort-exports */
import { IGeoGuesserGameContext } from '@itdl/game-portal/features/games/geo-guesser-game/entities';

export namespace GpllGeoGuesserStoreActions {
    // eslint-disable-next-line sort-exports/sort-exports
    export class ClearGameContext {
        static readonly type = '[Geo Guesser Game] Clear Game Context';
    }

    export class RefreshGameContext {
        static readonly type = '[Geo Guesser Game] Refresh Context';
        constructor(public gameId: string) {}
    }

    export class StoreGameContext {
        static readonly type = '[Geo Guesser Game] Store Context';
        constructor(public gameContext: IGeoGuesserGameContext | undefined) {}
    }
}
