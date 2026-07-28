import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';
import { GpllCreateGameDialogModule } from '@itdl/game-portal/features/game';
import { GpllPulseLoaderComponent } from '@itdl/game-portal/ui';
import { ShrlServerLoaderModule } from '@itdl/shared/ui';

import { GpllGameDefinitionItemComponent } from './game-definition-item.component';

@NgModule({
    imports: [
        CommonModule,
        GpllCreateGameDialogModule,
        MatButtonModule,
        MatCardModule,
        RouterModule,
        ShrlServerLoaderModule.withLoaderComponent(GpllPulseLoaderComponent),
    ],
    exports: [GpllGameDefinitionItemComponent],
    declarations: [GpllGameDefinitionItemComponent],
    providers: [],
})
export class GpllGameDefinitionItemModule {}
