import { IPlayer } from '../interfaces/player.interface';

export class Player implements IPlayer {
    nickname!: string;
    avatarType!: number;
}
