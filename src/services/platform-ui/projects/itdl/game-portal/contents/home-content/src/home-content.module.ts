import { CdkDrag } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { GpllUserProfileComponent } from '@itdl/game-portal/features/auth';
import {
    GpllStripeDonateButtonComponent,
    GpllStripeExpressCheckoutComponent,
} from '@itdl/game-portal/features/payment/stripe-integration';
import { GpllLoaderComponent, GpllThemeTogglerComponent } from '@itdl/game-portal/ui';
import { ShrlServerLoaderComponentTypeToken, ShrlServerLoaderModule } from '@itdl/shared/ui';
import { EditorModule } from '@tinymce/tinymce-angular';

import {
    GpllProjectItemContainerListFetcherComponent,
    GpllProjectItemContainerListViewComponent,
} from '@itdl/game-portal/features/project-item-container';
import { ShrlFooterTemplateModule, ShrlWithButtonToScrollModule } from '@itdl/shared/layout';
import { ShrlNavigationModule } from '@itdl/shared/navigation';
import { GpllDonationSectionComponent } from './donation-section/donation-section.component';
import { GpllEventsSectionComponent } from './events-section/events-section.component';
import { GpllFeaturesSectionComponent } from './features-section/features-section.component';
import { GpllFeedbackSectionComponent } from './feedback-section/feedback-section.component';
import { GpllFooterSectionComponent } from './game-jam/footer-section/footer-section.component';
import { GpllGoalsSectionComponent } from './game-jam/goals-section/goals-section.component';
import { GpllJoinSectionComponent } from './game-jam/join-section/join-section.component';
import { GpllMainBannerSectionComponent } from './game-jam/main-banner-section/main-banner-section.component';
import { GpllProjectDescriptionSectionComponent } from './game-jam/project-description-section/project-description-section.component';
import { GpllHomeContentComponent } from './home-content.component';
import { GpllNewsSectionComponent } from './news-section/news-section.component';
import { GpllStickyBoardSectionComponent } from './sticky-board-section/sticky-board-section.component';

@NgModule({
    imports: [
        CdkDrag,
        CommonModule,
        EditorModule,
        GpllDonationSectionComponent,
        GpllFeaturesSectionComponent,
        GpllFeedbackSectionComponent,
        GpllStickyBoardSectionComponent,
        GpllStripeDonateButtonComponent,
        GpllStripeExpressCheckoutComponent,
        GpllThemeTogglerComponent,
        GpllUserProfileComponent,
        MatButtonModule,
        MatCardModule,
        MatIconModule,
        ShrlWithButtonToScrollModule,
        MatSlideToggleModule,
        MatToolbarModule,
        ShrlNavigationModule,
        ShrlServerLoaderModule,
        GpllNewsSectionComponent,
        GpllEventsSectionComponent,
        GpllProjectItemContainerListFetcherComponent,
        GpllProjectItemContainerListViewComponent,
        GpllMainBannerSectionComponent,
        GpllGoalsSectionComponent,
        GpllProjectDescriptionSectionComponent,
        GpllJoinSectionComponent,
        GpllGoalsSectionComponent,
        GpllFooterSectionComponent,
        ShrlFooterTemplateModule,
    ],
    exports: [GpllHomeContentComponent],
    declarations: [GpllHomeContentComponent],
    providers: [{ provide: ShrlServerLoaderComponentTypeToken, useValue: GpllLoaderComponent }],
})
export class GpllHomeContentModule {}
