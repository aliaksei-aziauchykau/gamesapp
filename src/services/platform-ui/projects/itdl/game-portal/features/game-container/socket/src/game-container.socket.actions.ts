/* eslint-disable sort-exports/sort-exports */

export namespace GameContainerSocketActions {
    export class Create {
        static readonly type = '[Game Container] Create';
        constructor(public gameDefName: string, public gameId: string) {}
    }

    export class Start {
        static readonly type = '[Game Container] Start';
        constructor(public gameId: string) {}
    }

    export class Initialize {
        static readonly type = '[Game Container] Initialize';
        constructor(public gameId: string) {}
    }

    export class Stop {
        static readonly type = '[Game Container] Stop';
        constructor(public gameId: string) {}
    }

    export class Destroy {
        static readonly type = '[Game Container] Destroy';
        constructor(public gameId: string) {}
    }

    export class Fetch {
        static readonly type = '[Game Container] Fetch Game Container State';
        constructor(public gameId: string) {}
    }

    export class FetchContainerGameContext {
        static readonly type = '[Game Container] Fetch Container Game Context';
        constructor(public gameId: string) {}
    }

    export class Connect {
        static readonly type = '[Game Container] Connect';
        constructor(public gameId: string, public userId: string) {}
    }

    export class Disconnect {
        static readonly type = '[Game Container] Disconnect';
        constructor(public gameId: string, public userId: string) {}
    }
}
