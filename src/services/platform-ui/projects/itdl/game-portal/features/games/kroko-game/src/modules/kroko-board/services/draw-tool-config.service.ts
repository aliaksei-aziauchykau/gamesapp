import { Injectable } from '@angular/core';
import { DrawToolConfig } from '@itdl/game-portal/features/games/kroko-game/entities';
import { ReactiveValue } from '@itdl/shared/common';

@Injectable()
export class GpllDrawToolConfigService extends ReactiveValue<DrawToolConfig> {
    constructor() {
        super(new DrawToolConfig());
    }
}
