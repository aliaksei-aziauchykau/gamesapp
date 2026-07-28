import { Component, HostBinding, Input, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'gpll-input-control-style',
    templateUrl: 'input-control-style.component.html',
    styleUrls: ['input-control-style.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class GpllInputControlStyleComponent {
    @Input() hostClasses: string[] = [];

    @HostBinding('class')
    public get _hostClasses() {
        return [...this.hostClasses];
    }
}
