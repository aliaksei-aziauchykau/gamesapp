// element-ref.directive.ts
import { Directive, ElementRef } from '@angular/core';

@Directive({
    selector: '[shrlElementRef]',
    standalone: true,
    exportAs: 'shrlElementRef',
})
export class ShrlElementRefDirective {
    constructor(public elementRef: ElementRef) {}
}
