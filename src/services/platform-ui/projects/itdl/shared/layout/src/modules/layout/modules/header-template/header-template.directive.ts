/* eslint-disable @typescript-eslint/no-useless-constructor */
import { Directive, TemplateRef } from '@angular/core';

import { ShrlBaseTemplateDirective } from '../base-template/base-template.abstract';
import { ShrlHeaderTemplateReactiveValue } from './header-template.reactive-value.service';

@Directive({
    selector: '[shrlHeaderTemplate]',
})
export class ShrlHeaderTemplateDirective extends ShrlBaseTemplateDirective {
    constructor(templateRef: TemplateRef<unknown>, headerTemplateReactiveValue: ShrlHeaderTemplateReactiveValue) {
        super(templateRef, headerTemplateReactiveValue);
    }
}
