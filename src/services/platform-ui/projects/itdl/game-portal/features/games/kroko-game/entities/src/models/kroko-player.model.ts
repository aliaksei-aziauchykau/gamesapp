import { IKrokoPlayer } from '../interfaces/kroko-player.interface';

export class KrokoPlayer implements IKrokoPlayer {
    public userId!: string;
    public nickname!: string;
    public connectionIds: string[] = [];
}
