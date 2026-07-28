import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

import { GpllCreateGameDialogModule } from '../create-game-dialog/create-game-dialog.module';
import { GpllCreateGameButtonComponent } from './create-game-button.component';

@NgModule({
    imports: [CommonModule, GpllCreateGameDialogModule, MatButtonModule],
    declarations: [GpllCreateGameButtonComponent],
    exports: [GpllCreateGameButtonComponent],
})
export class GpllCreateGameButtonModule {}
