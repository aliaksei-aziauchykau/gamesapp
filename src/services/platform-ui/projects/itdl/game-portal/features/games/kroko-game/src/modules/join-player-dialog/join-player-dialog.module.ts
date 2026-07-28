import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { GpllJoinPlayerDialogComponent } from './join-player-dialog.component';
import { GpllJoinPlayerDialogService } from './join-player-dialog.service';

@NgModule({
    declarations: [GpllJoinPlayerDialogComponent],
    providers: [GpllJoinPlayerDialogService],
    imports: [CommonModule, FormsModule, MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule],
})
export class GpllJoinPlayerDialogModule {}
