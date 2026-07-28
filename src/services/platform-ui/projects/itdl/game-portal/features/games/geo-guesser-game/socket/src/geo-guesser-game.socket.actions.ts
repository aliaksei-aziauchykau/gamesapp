import { IKrokoGameSettings } from '@itdl/game-portal/features/games/kroko-game/entities';

/* eslint-disable sort-exports/sort-exports */
export namespace GpllGeoGuesserGameSocketActions {
    export class JoinPlayer {
        public gameId!: string;
        public playerName!: string;
        public userId!: string;

        static readonly type = '[Geo Guesser Game][Socket] Join Player';
        constructor(command: JoinPlayer) {
            Object.assign(this, command);
        }
    }

    export class LeavePlayer {
        public gameId!: string;
        public userId!: string;

        static readonly type = '[Geo Guesser Game][Socket] Leave Player';
        constructor(command: LeavePlayer) {
            Object.assign(this, command);
        }
    }

    export class StartGame {
        static readonly type = '[Geo Guesser Game][Socket] Start Game';
        constructor(public gameId: string) {}
    }

    export class SaveGameSettings {
        static readonly type = '[Geo Guesser Game][Socket] Save Game Settings';
        constructor(public gameSettings: IKrokoGameSettings) {}
    }

    export class FetchGameContext {
        static readonly type = '[Geo Guesser Game][Socket] Fetch Context';
        constructor(public gameId: string) {}
    }

    export class FetchGameSettings {
        static readonly type = '[Geo Guesser Game][Socket] Fetch Game Settings';
        constructor(public gameId: string) {}
    }

    export class RefreshGameContext {
        static readonly type = '[Kroko Game] Refresh Context';
        constructor(public gameId: string) {}
    }
}
