import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule, Type } from '@angular/core';

import { ShrlServerLoaderDirective } from './server-loader.directive';
import { ShrlServerLoaderComponentTypeToken } from './server-loader.token';

@NgModule({
    declarations: [ShrlServerLoaderDirective],
    exports: [ShrlServerLoaderDirective],
    imports: [CommonModule],
})
export class ShrlServerLoaderModule {
    public static withLoaderComponent(componentType: Type<unknown>): ModuleWithProviders<ShrlServerLoaderModule> {
        return {
            ngModule: ShrlServerLoaderModule,
            providers: [{ provide: ShrlServerLoaderComponentTypeToken, useValue: componentType }],
        };
    }
}
