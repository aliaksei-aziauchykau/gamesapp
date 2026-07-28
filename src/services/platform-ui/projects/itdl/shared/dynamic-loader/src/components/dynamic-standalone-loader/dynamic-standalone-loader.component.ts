import { Component, inject, Injector, Input } from '@angular/core';
import { RxState } from '@rx-angular/state';
import { from, map, switchMap } from 'rxjs';

import { CommonModule } from '@angular/common';
import { RxLet } from '@rx-angular/template/let';
import { IDynamicStandaloneRenderConfiguration } from '../../interfaces/dynamic-standalone-render-configuration.interface';

@Component({
    selector: 'shrl-dynamic-standalone-loader',
    templateUrl: 'dynamic-standalone-loader.component.html',
    standalone: true,
    imports: [CommonModule, RxLet],
    providers: [RxState],
})
export class ShrlDynamicStandaloneLoaderComponent {
    @Input() public set config(configuration: IDynamicStandaloneRenderConfiguration) {
        this.state.set(configuration);
    }

    private readonly injector = inject(Injector);

    public readonly config$ = this.state.select().pipe(
        switchMap((config) => {
            const module = config.loadModule();
            const module$ = from(Promise.resolve(module));
            return module$.pipe(map((module) => ({ module, config })));
        }),
        map(({ module, config }) => {
            const entryComponent = (module as any)?.entryComponent;
            const injector = config.injector ?? this.injector;
            return {
                entity: module,
                injector: (config.providersFn && config.providersFn(injector)) || injector,
                inputs: config.inputs,
            };
        }),
    );

    constructor(private readonly state: RxState<IDynamicStandaloneRenderConfiguration>) {}
}
