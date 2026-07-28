import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';

export interface IServerData {
    gamedefs?: GameDefinition[];
    envVars: { [key in string]: string };
}
