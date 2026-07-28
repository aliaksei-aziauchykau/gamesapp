import { Component, inject, Input, OnDestroy, OnInit } from '@angular/core';
import { GpllCellViewsGridProviderToken } from '../../column-provider/cell-views-grid.provider.token';

@Component({ selector: '', template: '' })
export abstract class CellView implements OnInit, OnDestroy {
    @Input() dataRow!: any;

    protected readonly cellViewsGridProvider = inject(GpllCellViewsGridProviderToken);

    ngOnInit(): void {
        this.cellViewsGridProvider.addCell(this.dataRow, this);
    }

    ngOnDestroy(): void {
        this.cellViewsGridProvider.removeCell(this.dataRow, this);
    }
}
