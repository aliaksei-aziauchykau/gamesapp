import { IGeoGuesserPlayer } from '../interfaces/geo-guesser-player.interface';

export class GeoGuesserPlayer implements IGeoGuesserPlayer {
    public userId!: string;
    public nickname!: string;
    public connectionIds: string[] = [];
}
