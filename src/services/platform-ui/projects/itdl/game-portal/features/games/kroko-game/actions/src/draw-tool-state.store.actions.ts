/* eslint-disable sort-exports/sort-exports */
import { DrawShapeConfig, IDrawToolState } from '@itdl/game-portal/features/games/kroko-game/entities';

// Rename to History
export namespace DrawToolStateActions {
    export class StoreInitialState {
        static readonly type = '[Draw Tool State] Store Initial State';
        constructor(public drawToolState: IDrawToolState) {}
    }

    export class ClearShapes {
        static readonly type = '[Draw Tool State] Clear Shapes';
    }

    export class AddShape {
        static readonly type = '[Draw Tool State] Add Shape';
        constructor(public shapeConfig: DrawShapeConfig) {}
    }

    export class ReplaceLastShape {
        static readonly type = '[Draw Tool State] Replace Last Shape';
        constructor(public shapeConfig: DrawShapeConfig) {}
    }

    export class StoreActionToHistory {
        static readonly type = '[Draw Tool State] Store Action To History';
        constructor(public action: () => void) {}
    }

    export class StoreActionAsLastToHistory {
        static readonly type = '[Draw Tool State] Store Action As Last To History';
        constructor(public action: () => void) {}
    }

    export class ClearHistory {
        static readonly type = '[Draw Tool State] Clear History';
    }

    export class Undo {
        static readonly type = '[Draw Tool State] Undo';
        constructor(public nextStateFn: (state: IDrawToolState) => void) {}
    }

    export class Redo {
        static readonly type = '[Draw Tool State] Redo';
        constructor(public nextStateFn: (state: IDrawToolState) => void) {}
    }

    export class ApplyHistoryActions {
        static readonly type = '[Draw Tool State] Apply History Actions';
    }
}
