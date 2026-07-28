import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllGamesContentComponent } from '@itdl/game-portal/contents/games-content';
import { GpllGameModule } from '@itdl/game-portal/features/game';

import { GplGamesPageComponent } from './games-page.component';
import { GplGamesPageRoutingModule } from './games-page.routing.module';

@NgModule({
    declarations: [GplGamesPageComponent],
    imports: [CommonModule, GplGamesPageRoutingModule, GpllGameModule, GpllGamesContentComponent],
})
export class GplGamesPageModule {}
