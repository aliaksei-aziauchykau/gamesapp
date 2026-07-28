import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { extend } from 'lodash-es';
import { ColumnDefGrid } from './column-def-grid.abstract.component';

@Component({
    selector: '',
    templateUrl: './default-column-def-grid.component.html',
    styleUrls: ['./default-column-def-grid.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule],
})
export class GpllDefaultColumnDefGridComponent extends ColumnDefGrid {
    public extend = extend;
}
