import { Component, HostBinding, Input, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'gpll-checkbox-control-style',
    templateUrl: 'checkbox-control-style.component.html',
    styleUrls: ['checkbox-control-style.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class GpllCheckboxControlStyleComponent {
    @Input() hostClasses: string[] = [];

    @HostBinding('class')
    public get _hostClasses() {
        return [...this.hostClasses];
    }
}
