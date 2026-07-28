/* eslint-disable sort-exports/sort-exports */
import { IGameContainerState, IGameContext } from '@itdl/game-portal/features/game-container/entities';

export namespace GameContainerStoreActions {
    export class Store {
        static readonly type = '[Game Container] Store Game Container State';
        constructor(public state: IGameContainerState) {}
    }

    export class StoreContainerGameContext {
        static readonly type = '[Game Container] Store Container Game Context';
        constructor(public gameContext: IGameContext) {}
    }

    export class StoreGameStateChanged {
        static readonly type = '[Game Container] Store Game State Changed';
        constructor(public gameStateChange: string) {}
    }
}
