import { Component, inject, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { SubmitApiFormActionResult } from '@itdl/game-portal/ui/api-form/form';

import { MatTable } from '@angular/material/table';
import { GridConfig } from './models/grid-config.model';
import { GpllCellViewsGridProviderToken } from './modules/column-provider/cell-views-grid.provider.token';
import { DefaultCellViewsGridProvider } from './modules/column-provider/default-cell-views-grid-provider.interface';

type State = {
    submitApiFormActionResult: SubmitApiFormActionResult;
};

@Component({
    selector: 'gpll-grid',
    templateUrl: './grid.component.html',
    styleUrls: ['./grid.component.scss'],
    providers: [{ provide: GpllCellViewsGridProviderToken, useClass: DefaultCellViewsGridProvider }],
})
export class GpllGridComponent implements OnInit {
    @Input() config!: GridConfig;
    @Input() data: unknown[] = [];

    @ViewChild(MatTable, { static: true }) table!: MatTable<unknown>;

    public readonly injector = inject(Injector);

    ngOnInit(): void {
        console.log('dada', this.table);
    }
}
