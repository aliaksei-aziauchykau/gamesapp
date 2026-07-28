import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { GpllSelectWordDialogComponent } from './select-word-dialog.component';

@Injectable()
export class GpllSelectWordDialogService {
    constructor(public dialog: MatDialog) {}

    public open(words: string[] = []) {
        return this.dialog.open(GpllSelectWordDialogComponent, {
            disableClose: true,
            data: { words },
        });
    }
}
