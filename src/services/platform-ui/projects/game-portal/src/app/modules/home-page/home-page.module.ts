import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllHomeContentModule } from '@itdl/game-portal/contents/home-content';

import { ShrlHeaderTemplateModule } from '@itdl/shared/layout';
import { ShrlFooterTemplateModule } from '@itdl/shared/layout/src/modules/layout/modules/footer-template/footer-template.module';
import { GplHomePageComponent } from './home-page.component';
import { GplHomePageRoutingModule } from './home-page.routing.module';

@NgModule({
    declarations: [GplHomePageComponent],
    imports: [
        CommonModule,
        GplHomePageRoutingModule,
        GpllHomeContentModule,
        ShrlFooterTemplateModule,
        ShrlHeaderTemplateModule,
    ],
    // imports: [CommonModule, GplHomePageRoutingModule],
})
export class GplHomePageModule {}
