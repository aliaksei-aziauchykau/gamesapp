import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { GpllCountdownTimerComponent } from '@itdl/game-portal/common/components';
import { GpllSelectControlStyleModule } from '@itdl/game-portal/ui/api-form/controls/select/select-control-style';
import { ShrlFormDataSyncModule } from '@itdl/shared/forms/form-data-sync';
import { RxLet } from '@rx-angular/template/let';
import { NgxColorsModule } from 'ngx-colors';

import { GpllKrokoBoardToolsComponent } from './kroko-board-tools.component';

@NgModule({
    imports: [
        CommonModule,
        GpllCountdownTimerComponent,
        GpllSelectControlStyleModule,
        MatButtonModule,
        MatCardModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatMenuModule,
        MatSelectModule,
        NgxColorsModule,
        ReactiveFormsModule,
        RxLet,
        ShrlFormDataSyncModule,
    ],
    exports: [GpllKrokoBoardToolsComponent],
    declarations: [GpllKrokoBoardToolsComponent],
    providers: [],
})
export class GpllKrokoBoardToolsModule {}
