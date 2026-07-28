export interface IDrawToolEventHandler {
    onMouseUp(event: MouseEvent): void;
    onMouseMove(event: MouseEvent): void;
    onMouseDown(event: MouseEvent): void;
    onTouchStart(event: TouchEvent): void;
    onTouchMove(event: TouchEvent): void;
    onTouchEnd(event: TouchEvent): void;
}
