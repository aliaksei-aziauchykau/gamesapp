/* eslint-disable @typescript-eslint/explicit-member-accessibility */
import { Directive, OnDestroy, TemplateRef } from '@angular/core';
import { ReactiveValue } from '@itdl/shared/common';

@Directive({})
export abstract class ShrlBaseTemplateDirective implements OnDestroy {
    constructor(
        templateRef: TemplateRef<unknown>,
        protected readonly templateReactiveValue: ReactiveValue<TemplateRef<unknown>>,
    ) {
        this.templateReactiveValue.next(templateRef);
    }

    public ngOnDestroy(): void {
        this.templateReactiveValue.next(null);
    }
}
