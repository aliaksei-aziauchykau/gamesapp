import { inject, Injectable } from '@angular/core';
import { DrawToolType, IDrawToolEventHandler } from '@itdl/game-portal/features/games/kroko-game/entities';
import { CircleConfig } from 'konva/lib/shapes/Circle';
import { uniqueId } from 'lodash-es';

import { GpllDrawToolConfigService } from '../services/draw-tool-config.service';
import { GpllDrawToolStageService } from '../services/draw-tool-stage.service';
import { GpllDrawToolStateService } from '../services/draw-tool-state.service';

type FullCircleConfig = CircleConfig & { tool: DrawToolType; id: string };

@Injectable()
export class GpllCircleDrawToolService implements IDrawToolEventHandler {
    private readonly drawToolStateService = inject(GpllDrawToolStateService);
    private readonly drawToolStageService = inject(GpllDrawToolStageService);
    private readonly drawToolConfigService = inject(GpllDrawToolConfigService);

    private isDrawing = false;

    public baseCircleConfig = {
        stroke: '#ffffff',
        strokeWidth: 10,
        tension: 0.7,
        lineCap: 'round',
        lineJoin: 'round',
        radius: 10,
        // globalCompositeOperation: 'source-over',
    } as CircleConfig;

    private initialX = 0;
    private initialY = 0;

    public onMouseDown(): void {
        this.isDrawing = true;
        const stage = this.drawToolStageService.valueSafe;
        const point = stage.getPointerPosition();

        this.initialX = point?.x ?? 0;
        this.initialY = point?.y ?? 0;

        this.drawToolStateService
            .addShape({
                tool: 'circle',
                id: uniqueId(),
                x: point?.x,
                y: point?.y,
                ...this.baseCircleConfig,
                stroke: this.drawToolConfigService.valueSafe.strokeColor,
                radius: 0.1,
            })
            .addToHistory();
    }

    public onMouseMove(): void {
        if (!this.isDrawing) {
            return;
        }

        const stage = this.drawToolStageService.valueSafe;
        const shapes = this.drawToolStateService.shapes;
        const point = stage.getPointerPosition();

        if (!point) throw new Error('Point is not defined');

        const lastCircle = shapes[shapes.length - 1] as FullCircleConfig;

        const radius = Math.sqrt(Math.pow(point.x - this.initialX, 2) + Math.pow(point.y - this.initialY, 2));
        lastCircle.radius = radius;

        this.drawToolStateService.replaceLastShape(lastCircle);
    }

    public onMouseUp(): void {
        if (!this.isDrawing) return;

        const shapes = this.drawToolStateService.shapes;
        const lastCircle = shapes[shapes.length - 1] as FullCircleConfig;

        this.drawToolStateService.replaceLastShape(lastCircle).addToAsLastHistory();

        this.isDrawing = false;
    }

    public onTouchStart(event: TouchEvent): void {
        event.preventDefault();
        this.onMouseDown();
    }

    public onTouchMove(event: TouchEvent): void {
        event.preventDefault();
        this.onMouseMove();
    }

    public onTouchEnd(event: TouchEvent): void {
        event.preventDefault();
        this.onMouseUp();
    }
}
