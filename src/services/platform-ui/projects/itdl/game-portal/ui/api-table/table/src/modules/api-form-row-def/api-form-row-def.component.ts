import { CommonModule } from '@angular/common';
import { Component, ViewEncapsulation } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { RowDefGridComponent } from '@itdl/game-portal/ui/grid';

@Component({
    selector: '',
    templateUrl: './api-form-row-def.component.html',
    styleUrls: ['./api-form-row-def.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule],
    encapsulation: ViewEncapsulation.None,
})
export class GpllApiFormRowDefGridComponent extends RowDefGridComponent {}
