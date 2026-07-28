import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { GpllJoinPlayerDialogComponent } from './join-player-dialog.component';

@Injectable()
export class GpllJoinPlayerDialogService {
    constructor(public dialog: MatDialog) {}

    public openJoinPlayerDialog() {
        return this.dialog.open(GpllJoinPlayerDialogComponent, {
            disableClose: true,
            data: { name: '', animal: '' },
        });
    }
}
