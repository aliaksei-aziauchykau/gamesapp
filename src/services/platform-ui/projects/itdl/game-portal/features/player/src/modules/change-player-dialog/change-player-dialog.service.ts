import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Player } from '@itdl/game-portal/features/player/entities';
import { ReactiveValue } from '@itdl/shared/common';

import { GpllChangePlayerDialogComponent, GpllChangePlayerDialogData } from './change-player-dialog.component';

export type ChangePlayerMatDialogRef = MatDialogRef<GpllChangePlayerDialogComponent, Player>;

@Injectable()
export class GpllChangePlayerDialogService extends ReactiveValue<ChangePlayerMatDialogRef> {
    constructor(public dialog: MatDialog) {
        super();
    }

    public openDialog(player: Player) {
        const dialog = this.dialog.open<GpllChangePlayerDialogComponent, GpllChangePlayerDialogData, Player>(
            GpllChangePlayerDialogComponent,
            {
                data: { player: player },
            },
        );

        this.next(dialog);

        return dialog;
    }
}
