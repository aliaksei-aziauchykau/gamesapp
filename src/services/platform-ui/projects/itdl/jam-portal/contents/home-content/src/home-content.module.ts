import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ShrlFooterTemplateModule, ShrlWithButtonToScrollModule } from '@itdl/shared/layout';
import { ShrlNavigationModule } from '@itdl/shared/navigation';
import { JpllFooterSectionComponent } from './footer-section/footer-section.component';
import { JpllGoalsSectionComponent } from './goals-section/goals-section.component';
import { JpllHomeContentComponent } from './home-content.component';
import { JpllJoinSectionComponent } from './join-section/join-section.component';
import { JpllMainBannerSectionComponent } from './main-banner-section/main-banner-section.component';
import { JpllProjectDescriptionSectionComponent } from './project-description-section/project-description-section.component';

@NgModule({
    imports: [
        CommonModule,
        ShrlWithButtonToScrollModule,
        ShrlNavigationModule,
        JpllMainBannerSectionComponent,
        JpllGoalsSectionComponent,
        JpllProjectDescriptionSectionComponent,
        JpllJoinSectionComponent,
        JpllGoalsSectionComponent,
        JpllFooterSectionComponent,
        ShrlFooterTemplateModule,
    ],
    exports: [JpllHomeContentComponent],
    declarations: [JpllHomeContentComponent],
    providers: [],
})
export class JpllHomeContentModule {}
