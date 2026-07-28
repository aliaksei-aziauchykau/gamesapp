import { Component, Input } from '@angular/core';

import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';

@Component({
    selector: '',
    templateUrl: './default-header-cell-view.component.html',
    styleUrls: ['./default-header-cell-view.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule],
})
export class GpllDefaultHeaderCellViewComponent {
    @Input() public name!: string;
}
