import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllGameContentModule } from '@itdl/game-portal/contents/game-content';
import { GpllGameModule } from '@itdl/game-portal/features/game';
import { GpllGameDefinitionModule } from '@itdl/game-portal/features/game-definition';

import { GplGamePageComponent } from './game-page.component';
import { GplGamePageRoutingModule } from './game-page.routing.module';

@NgModule({
    declarations: [GplGamePageComponent],
    imports: [CommonModule, GplGamePageRoutingModule, GpllGameContentModule, GpllGameDefinitionModule, GpllGameModule],
})
export class GplGamePageModule {}
