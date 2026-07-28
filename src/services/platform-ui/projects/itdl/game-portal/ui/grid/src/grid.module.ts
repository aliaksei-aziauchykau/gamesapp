import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';

import { ShrlDynamicStandaloneLoaderComponent } from '@itdl/shared/dynamic-loader';
import { GpllGridViewComponent } from './grid-view/grid-view.component';
import { GpllGridComponent } from './grid.component';
import { GpllGridConfigBuilder } from './services/grid-config.builder';

@NgModule({
    imports: [CommonModule, MatTableModule, ShrlDynamicStandaloneLoaderComponent],
    exports: [GpllGridComponent],
    declarations: [GpllGridComponent, GpllGridViewComponent],
    providers: [GpllGridConfigBuilder],
})
export class GpllGridModule {}
