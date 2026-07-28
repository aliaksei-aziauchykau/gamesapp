import { ShapeConfig } from 'konva/lib/Shape';

import { DrawToolType } from '../../models/draw-tool/draw-tool-config.model';

export type DrawShapeConfig = ShapeConfig & { tool: DrawToolType; id: string };

export interface IDrawToolState {
    isSkipForDrawing?: boolean;
    shapes: DrawShapeConfig[] | null;
}
