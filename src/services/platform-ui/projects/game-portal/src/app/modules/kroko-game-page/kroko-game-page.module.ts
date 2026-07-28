import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GplKrokoGamePageComponent } from './kroko-game-page.component';
import { GplKrokoGamePageRoutingModule } from './kroko-game-page.routing.module';

@NgModule({
    declarations: [GplKrokoGamePageComponent],
    imports: [CommonModule, GplKrokoGamePageRoutingModule],
})
export class GplKrokoGamePageModule {}
