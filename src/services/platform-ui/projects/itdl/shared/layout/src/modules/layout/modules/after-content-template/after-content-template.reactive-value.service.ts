import { Injectable, TemplateRef } from '@angular/core';
import { ReactiveValue } from '@itdl/shared/common';

@Injectable({ providedIn: 'root' })
export class ShrlAfterContentTemplateReactiveValue extends ReactiveValue<TemplateRef<unknown>> {
    constructor() {
        super(null);
    }
}
