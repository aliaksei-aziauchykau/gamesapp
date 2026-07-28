import { AfterContentInit, Component, ContentChild, HostBinding, Input } from '@angular/core';
import { MatSelect } from '@angular/material/select';

@Component({
    selector: 'gpll-select-control-style',
    templateUrl: 'select-control-style.component.html',
    styleUrls: ['select-control-style.component.scss'],
})
export class GpllSelectControlStyleComponent implements AfterContentInit {
    @ContentChild(MatSelect) matSelect!: MatSelect;
    @Input() panelClass!: string;
    @Input() hostClasses: string[] = [];

    @HostBinding('class')
    public get _hostClasses() {
        return [...this.hostClasses];
    }

    public ngAfterContentInit(): void {
        this.matSelect.panelClass = `gpll-select-control__panel--${this.panelClass}`;
    }
}
