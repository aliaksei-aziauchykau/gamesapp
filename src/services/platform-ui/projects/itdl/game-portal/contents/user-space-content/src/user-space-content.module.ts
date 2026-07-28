import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { GpllPanelComponent } from '@itdl/game-portal/ui/panel';
import { GpllUserSpaceContentComponent } from './user-space-content.component';
import { GplUserSpaceContentRoutingModule } from './user-space-content.routing.module';

@NgModule({
    imports: [
        CommonModule,
        // RxLet,
        // ShrlDynamicLoaderModule.configure(lobbyContentLazyModulesConfig),
        MatButtonModule,
        MatSidenavModule,
        MatListModule,
        GpllTranslationModule,
        MatIconModule,
        GplUserSpaceContentRoutingModule,
        GpllPanelComponent,
    ],
    exports: [],
    declarations: [GpllUserSpaceContentComponent],
    providers: [],
})
export class GpllUserSpaceContentModule {}
