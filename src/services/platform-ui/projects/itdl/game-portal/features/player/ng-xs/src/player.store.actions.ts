import { Player } from '@itdl/game-portal/features/player/entities';

export namespace PlayerStoreActions {
    // eslint-disable-next-line sort-exports/sort-exports
    export class GeneratePlayerNickname {
        static readonly type = '[Player] Generate Player Nickname';
    }

    export class StorePlayerNickname {
        static readonly type = '[Player] Store Player Nickname';
        constructor(public playerNickname: string) {}
    }

    export class StorePlayer {
        static readonly type = '[Player] Store Player';
        constructor(public player: Player) {}
    }
}
