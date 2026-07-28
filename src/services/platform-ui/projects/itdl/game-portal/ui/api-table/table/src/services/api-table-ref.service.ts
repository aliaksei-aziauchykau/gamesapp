import { Injectable } from '@angular/core';

import { ReactiveValue } from '@itdl/shared/common';
import { GpllApiTableComponent } from '../api-table.component';

@Injectable()
export class GpllApiTableRefService extends ReactiveValue<GpllApiTableComponent> {
    constructor() {
        super();
    }
}
