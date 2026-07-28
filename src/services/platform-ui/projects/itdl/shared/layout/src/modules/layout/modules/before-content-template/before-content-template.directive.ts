/* eslint-disable @typescript-eslint/no-useless-constructor */
import { Directive, TemplateRef } from '@angular/core';

import { ShrlBaseTemplateDirective } from '../base-template/base-template.abstract';
import { ShrlBeforeContentTemplateReactiveValue } from './before-content-template.reactive-value.service';

@Directive({
    selector: '[shrlBeforeContentTemplate]',
})
export class ShrlBeforeContentTemplateDirective extends ShrlBaseTemplateDirective {
    constructor(
        templateRef: TemplateRef<unknown>,
        beforeContentTemplateReactiveValue: ShrlBeforeContentTemplateReactiveValue,
    ) {
        super(templateRef, beforeContentTemplateReactiveValue);
    }
}
