import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ShrlDynamicLoaderModule } from '@itdl/shared/dynamic-loader';

import { GpllControlLoaderComponent } from './control-loader.component';
import { controlLoaderLazyModulesConfig } from './control-loader.lazy-modules';

@NgModule({
    imports: [CommonModule, ShrlDynamicLoaderModule.configure(controlLoaderLazyModulesConfig)],
    exports: [GpllControlLoaderComponent],
    declarations: [GpllControlLoaderComponent],
    providers: [],
})
export class GpllControlLoaderModule {}
