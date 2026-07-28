import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { GpllTranslationModule } from '@itdl/game-portal/translation';

import { GpllSelectWordDialogComponent } from './select-word-dialog.component';
import { GpllSelectWordDialogService } from './select-word-dialog.service';

@NgModule({
    declarations: [GpllSelectWordDialogComponent],
    providers: [GpllSelectWordDialogService],
    imports: [CommonModule, GpllTranslationModule, MatButtonModule, MatDialogModule],
})
export class GpllSelectWordDialogModule {}
