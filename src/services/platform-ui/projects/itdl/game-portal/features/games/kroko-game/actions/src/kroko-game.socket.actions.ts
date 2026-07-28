import { IKrokoGameSettings } from '@itdl/game-portal/features/games/kroko-game/entities';

/* eslint-disable sort-exports/sort-exports */
export namespace KrokoGameSocketActions {
    export class JoinPlayer {
        public gameId!: string;
        public playerName!: string;
        public userId!: string;

        static readonly type = '[Kroko Game] Join Player';
        constructor(command: JoinPlayer) {
            Object.assign(this, command);
        }
    }

    export class LeavePlayer {
        public gameId!: string;
        public userId!: string;

        static readonly type = '[Kroko Game] Leave Player';
        constructor(command: LeavePlayer) {
            Object.assign(this, command);
        }
    }

    export class GuessWord {
        public gameId!: string;
        public message!: string;
        public userId!: string;

        static readonly type = '[Kroko Game] Guess Word';
        constructor(command: GuessWord) {
            Object.assign(this, command);
        }
    }

    export class DrawWord {
        public gameId!: string;
        public userId!: string;
        public shapesJson!: string;

        static readonly type = '[Kroko Game] Draw Word';
        constructor(command: DrawWord) {
            Object.assign(this, command);
        }
    }

    export class StoreSelectedWord {
        public gameId!: string;
        public userId!: string;
        public wordSelected!: string;

        static readonly type = '[Kroko Game] Store Selected Word';
        constructor(command: StoreSelectedWord) {
            Object.assign(this, command);
        }
    }

    export class StartGame {
        static readonly type = '[Kroko Game] Start Game';
        constructor(public gameId: string) {}
    }

    export class SaveGameSettings {
        static readonly type = '[Kroko Game] Save Game Settings';
        constructor(public gameSettings: IKrokoGameSettings) {}
    }

    export class FetchGameContext {
        static readonly type = '[Kroko Game] Fetch Context';
        constructor(public gameId: string) {}
    }

    export class FetchGameSettings {
        static readonly type = '[Kroko Game] Fetch Game Settings';
        constructor(public gameId: string) {}
    }

    export class RefreshGameContext {
        static readonly type = '[Kroko Game] Refresh Context';
        constructor(public gameId: string) {}
    }
}
