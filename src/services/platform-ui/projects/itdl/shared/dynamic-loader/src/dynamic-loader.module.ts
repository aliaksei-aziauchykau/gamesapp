import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { RxLet } from '@rx-angular/template/let';

import { ShrlDynamicComponentLoaderComponent } from './components/dynamic-component-loader/dynamic-component-loader.component';
import { ShrlDynamicModuleLoaderComponent } from './components/dynamic-module-loader/dynamic-module-loader.component';
import { IDynamicLoaderConfiguration } from './interfaces/dynamic-loader-configuration.interface';
import { ShrlDynamicLoaderService } from './services/dynamic-loader.service';
import { DynamicLoaderConfigurationToken } from './tokens/dynamic-loader.tokens';

@NgModule({
    imports: [CommonModule, RxLet],
    declarations: [ShrlDynamicComponentLoaderComponent, ShrlDynamicModuleLoaderComponent],
    exports: [ShrlDynamicComponentLoaderComponent, ShrlDynamicModuleLoaderComponent],
    providers: [ShrlDynamicLoaderService],
})
export class ShrlDynamicLoaderModule {
    public static configure(config: IDynamicLoaderConfiguration): ModuleWithProviders<ShrlDynamicLoaderModule> {
        return {
            ngModule: ShrlDynamicLoaderModule,
            providers: [
                ShrlDynamicLoaderService,
                {
                    provide: DynamicLoaderConfigurationToken,
                    useValue: config,
                },
            ],
        };
    }
}
