import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ShrlFormDataSyncModule } from '@itdl/shared/forms/form-data-sync';

import { GpllChangePlayerDialogComponent } from './change-player-dialog.component';
import { GpllChangePlayerDialogService } from './change-player-dialog.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MatButtonModule,
        MatDialogModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatSelectModule,
        ReactiveFormsModule,
        ShrlFormDataSyncModule,
    ],
    exports: [GpllChangePlayerDialogComponent],
    declarations: [GpllChangePlayerDialogComponent],
    providers: [GpllChangePlayerDialogService],
})
export class GpllChangePlayerDialogModule {}
