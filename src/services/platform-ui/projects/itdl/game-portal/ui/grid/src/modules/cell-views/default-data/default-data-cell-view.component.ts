import { Component, Input, OnDestroy, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { get } from 'lodash-es';
import { CellView } from './cell-view.abstract.component';

@Component({
    selector: '',
    templateUrl: './default-data-cell-view.component.html',
    styleUrls: ['./default-data-cell-view.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule],
})
export class GpllDefaultDataCellViewComponent extends CellView implements OnInit, OnDestroy {
    @Input() public dataFieldPath!: string;

    get = get;

    public override ngOnInit(): void {
        super.ngOnInit();
        console.log('up');
    }

    public override ngOnDestroy(): void {
        super.ngOnDestroy();
        console.log('down');
    }
}
