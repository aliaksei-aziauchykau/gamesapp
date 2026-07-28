import { Component, HostBinding, Input, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'gpll-rte-control-style',
    templateUrl: 'rte-control-style.component.html',
    styleUrls: ['rte-control-style.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class GpllRteControlStyleComponent {
    @Input() hostClasses: string[] = [];

    @HostBinding('class')
    public get _hostClasses() {
        return [...this.hostClasses];
    }
}
