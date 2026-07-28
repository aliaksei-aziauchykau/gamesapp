import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

import { GpllGameItemComponent } from './game-item.component';

@NgModule({
    imports: [CommonModule, MatButtonModule, MatCardModule],
    declarations: [GpllGameItemComponent],
    exports: [GpllGameItemComponent],
})
export class GpllGameItemModule {}
