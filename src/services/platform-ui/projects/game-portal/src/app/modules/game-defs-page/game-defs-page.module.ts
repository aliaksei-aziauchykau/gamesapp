import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllGameDefsContentComponent } from '@itdl/game-portal/contents/game-defs-content';
import { GpllGameDefinitionModule } from '@itdl/game-portal/features/game-definition';

import { GplGameDefsPageComponent } from './game-defs-page.component';
import { GplGameDefsPageRoutingModule } from './game-defs-page.routing.module';

@NgModule({
    declarations: [GplGameDefsPageComponent],
    imports: [CommonModule, GplGameDefsPageRoutingModule, GpllGameDefinitionModule, GpllGameDefsContentComponent],
    // imports: [CommonModule, GplGamesPageRoutingModule],
})
export class GplGameDefsPageModule {}
