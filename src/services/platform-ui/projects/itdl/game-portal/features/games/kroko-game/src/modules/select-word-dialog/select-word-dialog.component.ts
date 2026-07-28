import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

interface DialogData {
    words: string[];
}

@Component({
    selector: 'gpll-select-word-dialog',
    templateUrl: './select-word-dialog.component.html',
    styleUrls: ['./select-word-dialog.component.scss'],
})
export class GpllSelectWordDialogComponent {
    constructor(
        public dialogRef: MatDialogRef<GpllSelectWordDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData,
    ) {}

    public onNoClick(): void {
        this.dialogRef.close();
    }
}
