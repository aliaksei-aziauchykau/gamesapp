import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

interface DialogData {
    playerName: string;
}

@Component({
    selector: 'gpll-join-player-dialog',
    templateUrl: './join-player-dialog.component.html',
    styleUrls: ['./join-player-dialog.component.scss'],
})
export class GpllJoinPlayerDialogComponent {
    constructor(
        public dialogRef: MatDialogRef<GpllJoinPlayerDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData,
    ) {}

    public onNoClick(): void {
        this.dialogRef.close();
    }
}
