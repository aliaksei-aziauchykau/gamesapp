import { DrawToolType } from '../../models/draw-tool/draw-tool-config.model';

export interface IDrawToolConfig {
    tool: DrawToolType;
    strokeColor: string;
    fillColor: string;
}
