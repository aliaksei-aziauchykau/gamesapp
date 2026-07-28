import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { JpllHomeContentModule } from '@itdl/jam-portal/contents/home-content';
import { ShrlHeaderTemplateModule } from '@itdl/shared/layout';
import { ShrlFooterTemplateModule } from '@itdl/shared/layout/src/modules/layout/modules/footer-template/footer-template.module';
import { JplHomePageComponent } from './home-page.component';
import { JplHomePageRoutingModule } from './home-page.routing.module';

@NgModule({
    declarations: [JplHomePageComponent],
    imports: [
        CommonModule,
        JplHomePageRoutingModule,
        JpllHomeContentModule,
        ShrlFooterTemplateModule,
        ShrlHeaderTemplateModule,
    ],
})
export class JplHomePageModule {}
