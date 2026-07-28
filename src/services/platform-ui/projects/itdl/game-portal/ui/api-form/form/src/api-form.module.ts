import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { GpllApiFormApiService } from '@itdl/game-portal/ui/api-form/api';
import { GpllControlLoaderModule } from '@itdl/game-portal/ui/api-form/controls';
import { ApiFormXSState } from '@itdl/game-portal/ui/api-form/ng-xs';
import { NgxsModule } from '@ngxs/store';

import { GpllOverlaySpinnerComponent, GpllPulseLoaderComponent } from '@itdl/game-portal/ui';
import { ShrlElementRefDirective } from '@itdl/shared/common';
import { RxLet } from '@rx-angular/template/let';
import { GpllApiFormComponent } from './api-form.component';
import { GpllApiFormFetcherComponent } from './modules/api-form-fetcher/api-form-fetcher.component';

@NgModule({
    imports: [
        CommonModule,
        GpllApiFormFetcherComponent,
        GpllControlLoaderModule,
        MatButtonModule,
        MatCardModule,
        MatCheckboxModule,
        MatInputModule,
        NgxsModule.forFeature([ApiFormXSState]),
        ReactiveFormsModule,
        GpllPulseLoaderComponent,
        GpllOverlaySpinnerComponent,
        RxLet,
        ShrlElementRefDirective,
    ],
    exports: [GpllApiFormComponent, GpllApiFormFetcherComponent],
    declarations: [GpllApiFormComponent],
    providers: [GpllApiFormApiService],
})
export class GpllApiFormModule {}
