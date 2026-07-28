import { Stage } from 'konva/lib/Stage';

import { IDrawToolConfig } from './draw-tool-config.interface';
import { IDrawToolEventHandler } from './draw-tool-event-handler.interface';
import { IDrawToolState } from './draw-tool-state.interface';

export interface IDrawToolContext {
    stage: Stage | null;
    state: IDrawToolState;
    config: IDrawToolConfig;
    tool: IDrawToolEventHandler | null;
}
