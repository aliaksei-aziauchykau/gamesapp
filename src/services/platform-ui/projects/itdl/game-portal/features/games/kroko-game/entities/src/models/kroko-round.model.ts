import { IKrokoMessage } from '../interfaces/kroko-message.interface';
import { IKrokoRound } from '../interfaces/kroko-round.interface';

export class KrokoRound implements IKrokoRound {
    messages: IKrokoMessage[] = [];
    shapesJson!: string;
    drawerUserId!: string;
    winnerUserId!: string | null;
}
