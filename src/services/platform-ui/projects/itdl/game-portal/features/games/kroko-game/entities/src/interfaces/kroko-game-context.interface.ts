import { IGameContext } from '@itdl/game-portal/features/game-container/entities';

import { IKrokoMessage } from './kroko-message.interface';
import { IKrokoPlayer } from './kroko-player.interface';
import { IKrokoRound } from './kroko-round.interface';

export interface IKrokoGameContext extends IGameContext {
    players: IKrokoPlayer[];
    rounds: IKrokoRound[];
    currentRound: IKrokoRound;
    timer: string | undefined;
    messages: IKrokoMessage[];
}
