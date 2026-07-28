import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export type GpllCreateGameDialogData = {
    name: string;
};

@Component({
    selector: 'gpll-create-game-dialog',
    templateUrl: './create-game-dialog.component.html',
    styleUrls: ['./create-game-dialog.component.scss'],
})
export class GpllCreateGameDialogComponent {
    constructor(
        public dialogRef: MatDialogRef<GpllCreateGameDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: GpllCreateGameDialogData,
    ) {}

    public onNoClick(): void {
        this.dialogRef.close();
    }
}
