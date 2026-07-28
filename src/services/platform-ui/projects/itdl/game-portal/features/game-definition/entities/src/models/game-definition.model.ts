import { Game } from '@itdl/game-portal/features/game/entities';

export class GameDefinition {
    id!: number;
    name!: string;
    description!: string;
    imageUrl!: string;
    imageAltUrl!: string;
    gamePageAppUrl!: string;
    games: Game[] = [];
}
