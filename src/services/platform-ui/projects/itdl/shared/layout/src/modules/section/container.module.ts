import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ContainerComponent } from './container.component';

@NgModule({
    imports: [CommonModule],
    exports: [ContainerComponent],
    declarations: [ContainerComponent],
    providers: [],
})
export class SectionModule {}
