import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Player } from '@itdl/game-portal/features/player/entities';
import { GpllFormControl, GpllFormGroup } from '@itdl/game-portal/forms';

export type GpllChangePlayerDialogData = {
    player?: Player;
};

@Component({
    selector: 'gpll-change-player-dialog',
    templateUrl: './change-player-dialog.component.html',
    styleUrls: ['./change-player-dialog.component.scss'],
})
export class GpllChangePlayerDialogComponent implements OnInit {
    constructor(
        public dialogRef: MatDialogRef<GpllChangePlayerDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public dialogData: GpllChangePlayerDialogData,
    ) {}

    public onNoClick(): void {
        this.dialogRef.close();
    }

    public model!: Player;
    public avatarTypes = [1, 2];

    public form!: GpllFormGroup;
    public nicknameControl!: GpllFormControl<string>;
    public avatarTypeControl!: GpllFormControl<number>;

    public ngOnInit(): void {
        this.model = this.dialogData.player ?? new Player();
        this.form = this.getForm();
    }

    private getForm() {
        this.nicknameControl = this.getNicknameControl();
        this.avatarTypeControl = this.getAvatarTypeControl();

        return GpllFormGroup.create({
            controls: {
                numberOfRounds: this.nicknameControl,
                avatarType: this.avatarTypeControl,
            },
        });
    }

    private getNicknameControl() {
        return GpllFormControl.create<string>({
            formState: this.model.nickname,
            formDataSyncModel: 'nickname',
        });
    }

    private getAvatarTypeControl() {
        return GpllFormControl.create<number>({
            formState: this.model.avatarType,
            formDataSyncModel: 'avatarType',
        });
    }
}
