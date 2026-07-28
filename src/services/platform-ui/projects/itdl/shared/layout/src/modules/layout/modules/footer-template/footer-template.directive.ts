/* eslint-disable @typescript-eslint/no-useless-constructor */
import { Directive, TemplateRef } from '@angular/core';

import { ShrlBaseTemplateDirective } from '../base-template/base-template.abstract';
import { ShrlFooterTemplateReactiveValue } from './footer-template.reactive-value.service';

@Directive({
    selector: '[shrlFooterTemplate]',
})
export class ShrlFooterTemplateDirective extends ShrlBaseTemplateDirective {
    constructor(templateRef: TemplateRef<unknown>, footerTemplateReactiveValue: ShrlFooterTemplateReactiveValue) {
        super(templateRef, footerTemplateReactiveValue);
    }
}
