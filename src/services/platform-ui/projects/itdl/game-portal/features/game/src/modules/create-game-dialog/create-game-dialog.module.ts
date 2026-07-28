import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { GpllCreateGameDialogComponent } from './create-game-dialog.component';
import { GpllCreateGameDialogService } from './create-game-dialog.service';

@NgModule({
    imports: [CommonModule, FormsModule, MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule],
    declarations: [GpllCreateGameDialogComponent],
    providers: [GpllCreateGameDialogService],
})
export class GpllCreateGameDialogModule {}
