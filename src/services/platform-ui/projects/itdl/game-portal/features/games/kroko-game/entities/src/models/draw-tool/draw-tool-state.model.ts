import { DrawShapeConfig, IDrawToolState } from '../../interfaces/draw-tool/draw-tool-state.interface';

export class DrawToolState implements IDrawToolState {
    isSkipForDrawing = true;
    shapes!: DrawShapeConfig[] | null;

    constructor(shapes: DrawShapeConfig[] | null = null) {
        this.shapes = shapes;
    }

    public get shapesSafe(): DrawShapeConfig[] {
        return this.shapes || [];
    }
}
