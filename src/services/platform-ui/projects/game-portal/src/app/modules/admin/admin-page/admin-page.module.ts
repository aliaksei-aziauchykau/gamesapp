import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllAdminContentModule } from '@itdl/game-portal/contents/admin';

import { GplAdminPageComponent } from './admin-page.component';
import { GplAdminPageRoutingModule } from './admin-page.routing.module';

@NgModule({
    declarations: [GplAdminPageComponent],
    imports: [CommonModule, GplAdminPageRoutingModule, GpllAdminContentModule],
    // imports: [CommonModule, GplGamesPageRoutingModule],
})
export class GplAdminPageModule {}
