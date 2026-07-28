import { Injectable } from '@angular/core';

import { ReactiveValue } from '@itdl/shared/common';
import { GpllApiFormComponent } from '../api-form.component';

@Injectable()
export class GpllApiFormRefService extends ReactiveValue<GpllApiFormComponent> {
    constructor() {
        super();
    }
}
