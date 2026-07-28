import { Injectable, TemplateRef } from '@angular/core';
import { ReactiveValue } from '@itdl/shared/common';

@Injectable({ providedIn: 'root' })
export class ShrlBeforeContentTemplateReactiveValue extends ReactiveValue<TemplateRef<unknown>> {
    constructor() {
        super(null);
    }
}
