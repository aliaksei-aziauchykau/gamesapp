import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { GpllApiFormModule } from '@itdl/game-portal/ui/api-form/form';
import { GpllApiTableApiService } from '@itdl/game-portal/ui/api-table/api';
import { ApiTableXSState } from '@itdl/game-portal/ui/api-table/ng-xs';
import { NgxsModule } from '@ngxs/store';

import { GpllGridModule } from '@itdl/game-portal/ui/grid';
import { ShrlWithProvidersComponent } from '@itdl/shared/modules/with-providers';
import { GpllApiTableComponent } from './api-table.component';
import { GpllApiTableFetcherComponent } from './modules/api-table-fetcher/api-table-fetcher.component';

@NgModule({
    imports: [
        CommonModule,
        GpllApiFormModule,
        GpllApiTableFetcherComponent,
        GpllGridModule,
        ShrlWithProvidersComponent,
        MatButtonModule,
        MatIconModule,
        MatTableModule,
        NgxsModule.forFeature([ApiTableXSState]),
    ],
    exports: [GpllApiTableComponent, GpllApiTableFetcherComponent],
    declarations: [GpllApiTableComponent],
    providers: [GpllApiTableApiService],
})
export class GpllApiTableModule {}
