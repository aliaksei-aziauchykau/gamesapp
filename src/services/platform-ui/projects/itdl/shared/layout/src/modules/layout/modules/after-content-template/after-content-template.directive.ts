/* eslint-disable @typescript-eslint/no-useless-constructor */
import { Directive, TemplateRef } from '@angular/core';

import { ShrlBaseTemplateDirective } from '../base-template/base-template.abstract';
import { ShrlAfterContentTemplateReactiveValue } from './after-content-template.reactive-value.service';

@Directive({
    selector: '[shrlFooterTemplate]',
})
export class ShrlAfterContentTemplateDirective extends ShrlBaseTemplateDirective {
    constructor(
        templateRef: TemplateRef<unknown>,
        afterContentTemplateReactiveValue: ShrlAfterContentTemplateReactiveValue,
    ) {
        super(templateRef, afterContentTemplateReactiveValue);
    }
}
