import { inject, Injectable } from '@angular/core';
import { DrawToolType, IDrawToolEventHandler } from '@itdl/game-portal/features/games/kroko-game/entities';
import { LineConfig } from 'konva/lib/shapes/Line';
import { uniqueId } from 'lodash-es';

import { GpllDrawToolConfigService } from '../services/draw-tool-config.service';
import { GpllDrawToolStageService } from '../services/draw-tool-stage.service';
import { GpllDrawToolStateService } from '../services/draw-tool-state.service';

type FullLineConfig = LineConfig & { tool: DrawToolType; id: string };
@Injectable()
export class GpllLineDrawToolService implements IDrawToolEventHandler {
    private isDrawing = false;

    private readonly drawToolStateService = inject(GpllDrawToolStateService);
    private readonly drawToolStageService = inject(GpllDrawToolStageService);
    private readonly drawToolConfigService = inject(GpllDrawToolConfigService);

    public lineBaseConfig = {
        stroke: '#ffffff',
        strokeWidth: 10,
        tension: 0.7,
        lineCap: 'round',
        lineJoin: 'round',
        // globalCompositeOperation: 'source-over',
    } as LineConfig;

    public onMouseDown(): void {
        this.isDrawing = true;
        const stage = this.drawToolStageService.valueSafe;
        const point = stage.getPointerPosition();

        this.drawToolStateService
            .addShape({
                tool: 'pen',
                id: uniqueId(),
                points: [point?.x, point?.y],
                ...this.lineBaseConfig,
                stroke: this.drawToolConfigService.valueSafe.strokeColor,
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

        const lastLine = shapes[shapes.length - 1] as FullLineConfig;
        // add point
        const points = lastLine.points ?? [];
        lastLine.points = [...points, point?.x ?? 0, point?.y ?? 0];

        this.drawToolStateService.replaceLastShape(lastLine);
    }

    public onMouseUp(): void {
        if (!this.isDrawing) return;

        const shapes = this.drawToolStateService.shapes;
        const lastLine = shapes[shapes.length - 1] as FullLineConfig;

        this.drawToolStateService.replaceLastShape(lastLine).addToAsLastHistory();

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
