import { IDrawToolConfig } from '../../interfaces/draw-tool/draw-tool-config.interface';

export class DrawToolConfig implements IDrawToolConfig {
    tool: DrawToolType = 'pen';
    strokeColor = '#FFFFFF';
    fillColor = '#FFFFFF';
}

export type DrawToolType = 'pen' | 'circle' | 'donught' | 'rectangle' | 'eraser';
