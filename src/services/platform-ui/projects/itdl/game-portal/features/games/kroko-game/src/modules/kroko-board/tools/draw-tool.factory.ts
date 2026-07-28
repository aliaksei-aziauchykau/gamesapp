import { inject, Injectable } from '@angular/core';
import { DrawToolType, IDrawToolEventHandler } from '@itdl/game-portal/features/games/kroko-game/entities';

import { GpllCircleDrawToolService } from './circle-draw-tool.service';
import { GpllDonughtDrawToolService } from './donught-draw-tool.service';
import { GpllLineDrawToolService } from './line-draw-tool.service';

@Injectable()
export class GpllDrawToolFactory {
    private readonly tools = new Map<DrawToolType, IDrawToolEventHandler>([
        ['pen', inject(GpllLineDrawToolService)],
        ['circle', inject(GpllCircleDrawToolService)],
        ['donught', inject(GpllDonughtDrawToolService)],
    ]);

    public get(toolType: DrawToolType) {
        const drawTool = this.tools.get(toolType);

        if (!drawTool) throw new Error(`Draw tool ${toolType} not found`);

        return drawTool;
    }
}
