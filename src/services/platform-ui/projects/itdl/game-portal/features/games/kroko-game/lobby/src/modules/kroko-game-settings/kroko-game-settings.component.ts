import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { IKrokoGameSettings } from '@itdl/game-portal/features/games/kroko-game/entities';
import { GpllFormControl, GpllFormGroup } from '@itdl/game-portal/forms';
import { ShrlFormDataSyncModule } from '@itdl/shared/forms/form-data-sync';

@Component({
    selector: 'gpll-kroko-game-settings',
    standalone: true,
    templateUrl: './kroko-game-settings.component.html',
    styleUrls: ['./kroko-game-settings.component.scss'],
    imports: [
        CommonModule,
        MatCardModule,
        ShrlFormDataSyncModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatSelectModule,
    ],
})
export class GpllKrokoGameSettingsComponent implements OnInit {
    @Input() model!: IKrokoGameSettings;

    public form!: GpllFormGroup;

    public numberOfRounds = new Array(5).fill(0).map((v, i) => i + 1);

    public numberOfRoundsControl!: GpllFormControl<number>;

    ngOnInit(): void {
        this.form = this.getForm();
    }

    private getForm() {
        this.numberOfRoundsControl = this.getNumberOfRoundsControl();

        return GpllFormGroup.create({
            controls: {
                numberOfRounds: this.numberOfRoundsControl,
            },
        });
    }

    private getNumberOfRoundsControl() {
        return GpllFormControl.create<number>({
            formState: this.model.numberOfRounds,
            formDataSyncModel: 'numberOfRounds',
        });
    }
}
