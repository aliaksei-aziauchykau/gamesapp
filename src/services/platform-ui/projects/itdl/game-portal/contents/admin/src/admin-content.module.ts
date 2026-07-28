import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllPulseLoaderComponent } from '@itdl/game-portal/ui';
import { ShrlServerLoaderModule } from '@itdl/shared/ui';
import { RxLet } from '@rx-angular/template/let';

import { GpllApiFormModule } from '@itdl/game-portal/ui/api-form/form';
import { GpllApiTableModule } from '@itdl/game-portal/ui/api-table/table';
import { GpllGridModule } from '@itdl/game-portal/ui/grid';
import { GpllAdminContentComponent } from './admin-content.component';

@NgModule({
    imports: [
        CommonModule,
        GpllApiFormModule,
        GpllApiTableModule,
        GpllGridModule,
        RxLet,
        ShrlServerLoaderModule.withLoaderComponent(GpllPulseLoaderComponent),
    ],
    exports: [GpllAdminContentComponent],
    declarations: [GpllAdminContentComponent],
    providers: [],
})
export class GpllAdminContentModule {}
