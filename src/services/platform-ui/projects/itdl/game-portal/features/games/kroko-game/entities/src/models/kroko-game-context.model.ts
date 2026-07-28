import { IKrokoGameContext } from '../interfaces/kroko-game-context.interface';
import { IKrokoMessage } from '../interfaces/kroko-message.interface';
import { IKrokoRound } from '../interfaces/kroko-round.interface';

export class KrokoGameContext implements IKrokoGameContext {
    public players = [];
    public gameId!: string;
    public rounds: IKrokoRound[] = [];
    public currentRound!: IKrokoRound;
    public timer: string | undefined = undefined;

    public messages: IKrokoMessage[] = [];
}
