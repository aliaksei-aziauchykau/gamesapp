import { Component, Input, TemplateRef, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'gpll-form-field',
    templateUrl: 'form-field.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class GpllFormFieldComponent {
    @Input() content!: TemplateRef<unknown>;
}
