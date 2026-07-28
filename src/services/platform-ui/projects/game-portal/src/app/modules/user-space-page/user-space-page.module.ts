import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllGameModule } from '@itdl/game-portal/features/game';
import { GpllGameDefinitionModule } from '@itdl/game-portal/features/game-definition';

import { GplUserSpacePageComponent } from './user-space-page.component';
import { GplUserSpacePageRoutingModule } from './user-space-page.routing.module';

@NgModule({
    declarations: [GplUserSpacePageComponent],
    imports: [
        CommonModule,
        GpllGameDefinitionModule,
        GpllGameModule,
        // GpllUserSpaceContentModule,
        GplUserSpacePageRoutingModule,
    ],
})
export class GplUserSpacePageModule {}
