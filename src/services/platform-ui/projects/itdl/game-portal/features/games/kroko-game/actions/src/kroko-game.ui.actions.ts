export namespace KrokoGameUiActions {
    // eslint-disable-next-line sort-exports/sort-exports
    export class CloseAllModalsUiAction {
        static readonly type = '[KrokoGame] CloseAllModalsUiAction';
    }

    export class RejoinPlayerUiAction {
        static readonly type = '[KrokoGame] RejoinPlayerUiAction';
    }

    export class ReloadShapesUiAction {
        static readonly type = '[KrokoGame] ReloadShapesUiAction';
    }

    export class SelectWordUiAction {
        static readonly type = '[KrokoGame] SelectWordUiAction';

        constructor(public readonly words: string[]) {}
    }

    export class ChangePlayerUiAction {
        static readonly type = '[KrokoGame] ChangePlayerUiAction';

        constructor(public readonly words: string[]) {}
    }
}
