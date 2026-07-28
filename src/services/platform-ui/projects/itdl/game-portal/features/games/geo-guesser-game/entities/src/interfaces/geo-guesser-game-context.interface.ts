import { IGameContext } from '@itdl/game-portal/features/game-container/entities';

import { IGeoGuesserPlayer } from './geo-guesser-player.interface';
import { IGeoGuesserRound } from './geo-guesser-round.interface';

export interface IGeoGuesserGameContext extends IGameContext {
    players: IGeoGuesserPlayer[];
    rounds: IGeoGuesserRound[];
    currentRound: IGeoGuesserRound;
    timer: string | undefined;
}
