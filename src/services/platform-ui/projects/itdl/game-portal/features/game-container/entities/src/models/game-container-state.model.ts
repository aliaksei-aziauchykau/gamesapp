import { IGameContainerState } from '../interfaces/game-container-state.interface';

export class GameContainerState implements IGameContainerState {
    isRunning = false;
    isCreated = false;
    currentGameStateName: string | null = null;
}
