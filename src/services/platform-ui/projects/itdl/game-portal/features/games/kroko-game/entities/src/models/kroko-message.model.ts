import { IKrokoMessage } from '../interfaces/kroko-message.interface';

export class KrokoMessage implements IKrokoMessage {
    playerName!: string;
    message!: string;
}
