import { CdkHeaderCellDef } from '@angular/cdk/table';
import { Directive } from '@angular/core';
import { MatHeaderCellDef } from '@angular/material/table';

@Directive({
    selector: '[gpllHeaderCellDef]',
    providers: [{ provide: CdkHeaderCellDef, useExisting: GpllHeaderCellDef }],
    standalone: true,
    exportAs: 'headerCellDef',
})
export class GpllHeaderCellDef extends MatHeaderCellDef {}
