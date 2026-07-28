import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllLobbyContentModule } from '@itdl/game-portal/contents/lobby-content';
import { GpllGameModule } from '@itdl/game-portal/features/game';
import { GpllGameDefinitionModule } from '@itdl/game-portal/features/game-definition';

import { GplLobbyPageComponent } from './lobby-page.component';
import { GplLobbyPageRoutingModule } from './lobby-page.routing.module';

@NgModule({
    declarations: [GplLobbyPageComponent],
    imports: [
        CommonModule,
        GpllGameDefinitionModule,
        GpllGameModule,
        GpllLobbyContentModule,
        GplLobbyPageRoutingModule,
    ],
})
export class GplLobbyPageModule {}
