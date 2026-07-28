import { Injectable } from '@angular/core';
import { ReactiveValue } from '@itdl/shared/common';
import { Stage } from 'konva/lib/Stage';

@Injectable()
export class GpllDrawToolStageService extends ReactiveValue<Stage> {
    constructor() {
        super(null);
    }
}
