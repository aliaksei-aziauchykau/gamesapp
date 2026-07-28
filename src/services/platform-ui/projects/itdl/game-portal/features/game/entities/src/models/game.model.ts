import { Entity } from '@itdl/game-portal/http';

export class Game extends Entity {
    id!: string;
    name!: string;
    gameDefinitionId!: number;
    code!: string;
}
