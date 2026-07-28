import { IGeoGuesserGameContext } from '../interfaces/geo-guesser-game-context.interface';
import { IGeoGuesserPlayer } from '../interfaces/geo-guesser-player.interface';
import { IGeoGuesserRound } from '../interfaces/geo-guesser-round.interface';

export class GeoGuesserGameContext implements IGeoGuesserGameContext {
    gameId!: string;
    players: IGeoGuesserPlayer[] = [];
    rounds: IGeoGuesserRound[] = [];
    currentRound!: IGeoGuesserRound;
    public timer: string | undefined = undefined;
}
