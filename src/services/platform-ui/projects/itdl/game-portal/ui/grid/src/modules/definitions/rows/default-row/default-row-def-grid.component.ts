import { Component } from '@angular/core';

import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { RowDefGridComponent } from './row-def-grid.abstract.component';

@Component({
    selector: '',
    templateUrl: './default-row-def-grid.component.html',
    styleUrls: ['./default-row-def-grid.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule],
})
export class GpllDefaultRowDefGridComponent extends RowDefGridComponent {}
