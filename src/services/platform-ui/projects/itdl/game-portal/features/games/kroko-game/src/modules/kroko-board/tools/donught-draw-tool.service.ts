import { inject, Injectable } from '@angular/core';
import { IDrawToolEventHandler } from '@itdl/game-portal/features/games/kroko-game/entities';
import { CircleConfig } from 'konva/lib/shapes/Circle';
import { uniqueId } from 'lodash-es';

import { GpllDrawToolConfigService } from '../services/draw-tool-config.service';
import { GpllDrawToolStageService } from '../services/draw-tool-stage.service';
import { GpllDrawToolStateService } from '../services/draw-tool-state.service';

@Injectable()
export class GpllDonughtDrawToolService implements IDrawToolEventHandler {
    private readonly drawToolStateService = inject(GpllDrawToolStateService);
    private readonly drawToolStageService = inject(GpllDrawToolStageService);
    private readonly drawToolConfigService = inject(GpllDrawToolConfigService);

    public baseCircleConfig = {
        stroke: '#ffffff',
        strokeWidth: 10,
        tension: 0.7,
        lineCap: 'round',
        lineJoin: 'round',
        radius: 10,
        // globalCompositeOperation: 'source-over',
    } as CircleConfig;

    public onMouseDown(): void {
        const stage = this.drawToolStageService.valueSafe;
        const point = stage.getPointerPosition();

        this.drawToolStateService
            .addShape({
                tool: 'circle',
                id: uniqueId(),
                x: point?.x,
                y: point?.y,
                ...this.baseCircleConfig,
                stroke: this.drawToolConfigService.valueSafe.strokeColor,
            })
            .addToHistory();
    }

    public onMouseMove(): void {
        return;
    }

    public onMouseUp(): void {
        return;
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
