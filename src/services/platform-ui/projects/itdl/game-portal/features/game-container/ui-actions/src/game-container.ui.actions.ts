/* eslint-disable sort-exports/sort-exports */

export namespace GameContainerUiActions {
    export class Run {
        static readonly type = '[Game Container] Run';
        constructor(public gameDefName: string, public gameId: string) {}
    }
}
