import { Component, Input } from '@angular/core';
import { RxState } from '@rx-angular/state';
import { map, switchMap, tap } from 'rxjs';

import { IDynamicRenderConfiguration } from '../../interfaces/dynamic-render-configuration.interface';
import { ShrlDynamicLoaderService } from '../../services/dynamic-loader.service';

@Component({
    selector: 'shrl-dynamic-module-loader',
    templateUrl: 'dynamic-module-loader.component.html',
    providers: [RxState],
})
export class ShrlDynamicModuleLoaderComponent {
    @Input() public set config(configuration: IDynamicRenderConfiguration) {
        this.state.set(configuration);
    }

    public readonly config$ = this.state.select().pipe(
        switchMap((config) =>
            this.dynamicLoaderService.getDynamicModuleRefAndComponent(config.alias).pipe(
                tap((mod) => console.log('re', mod)),
                map(({ module, entity, injector }) => ({
                    module: module as any,
                    entity: entity as any,
                    injector: config.injector || injector,
                    inputs: config.inputs,
                })),
            ),
        ),
    );

    constructor(
        private readonly state: RxState<IDynamicRenderConfiguration>,
        private readonly dynamicLoaderService: ShrlDynamicLoaderService,
    ) {}
}
