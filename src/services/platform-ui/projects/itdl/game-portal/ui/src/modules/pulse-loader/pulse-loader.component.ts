import { Component, HostBinding, Input } from '@angular/core';

@Component({
    selector: 'gpll-pulse-loader',
    templateUrl: 'pulse-loader.component.html',
    styleUrls: ['./pulse-loader.component.scss'],
    standalone: true,
    imports: [],
})
export class GpllPulseLoaderComponent {
    @Input() hostClasses: string[] = [];

    @HostBinding('class')
    public get _hostClasses() {
        return [...this.hostClasses];
    }
}
