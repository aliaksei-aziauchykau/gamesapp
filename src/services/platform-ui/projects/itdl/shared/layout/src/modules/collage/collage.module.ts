import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { CollageComponent } from './collage.component';

@NgModule({
    declarations: [CollageComponent],
    exports: [CollageComponent],
    imports: [CommonModule],
})
export class CollageModule {}
