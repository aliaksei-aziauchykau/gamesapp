import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { HeaderRowDefGridComponent } from './header-row-def-grid.abstract.component';

@Component({
    selector: '',
    templateUrl: './default-header-row-def-grid.component.html',
    styleUrls: ['./default-header-row-def-grid.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule],
})
export class GpllDefaultHeaderRowDefGridComponent extends HeaderRowDefGridComponent {}
