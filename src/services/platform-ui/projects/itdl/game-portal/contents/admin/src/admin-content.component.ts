import { Component, inject } from '@angular/core';
import { GpllGridConfigBuilder } from '@itdl/game-portal/ui/grid';

@Component({
    selector: 'gpll-admin-content',
    templateUrl: './admin-content.component.html',
    styleUrls: ['./admin-content.component.scss'],
    providers: [],
})
export class GpllAdminContentComponent {
    public data = [
        { name: 'test', description: 'test' },
        { name: 'de', description: 're' },
        { name: 'fe', description: 'ew' },
    ];

    private readonly displayedColumns = ['name', 'description'];
    public readonly config = inject(GpllGridConfigBuilder)
        .withDefaultHeaderRowDef(this.displayedColumns)
        .withDefaultRowDef(this.displayedColumns)
        .withDefaultColumn('name')
        .withDefaultColumn('description').config;

    constructor() {
        console.log(this.config);
    }
}
