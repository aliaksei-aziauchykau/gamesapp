import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ReactiveValue } from '@itdl/shared/common';

import { GpllCreateGameDialogComponent, GpllCreateGameDialogData } from './create-game-dialog.component';

export type CreateGameMatDialogRef = MatDialogRef<GpllCreateGameDialogComponent, GpllCreateGameDialogData>;

@Injectable()
export class GpllCreateGameDialogService extends ReactiveValue<CreateGameMatDialogRef> {
    constructor(public dialog: MatDialog) {
        super();
    }

    public openDialog() {
        const dialog = this.dialog.open<
            GpllCreateGameDialogComponent,
            GpllCreateGameDialogData,
            GpllCreateGameDialogData
        >(GpllCreateGameDialogComponent, {
            data: { name: '' },
        });

        this.next(dialog);
    }
}
