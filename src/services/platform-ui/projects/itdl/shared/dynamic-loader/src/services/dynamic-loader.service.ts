import { createNgModule, EnvironmentInjector, Inject, Injectable, Injector } from '@angular/core';
import { from, map, throwError } from 'rxjs';

import { IDynamicLoaderConfiguration } from '../interfaces/dynamic-loader-configuration.interface';
import { DynamicLoaderConfigurationToken } from '../tokens/dynamic-loader.tokens';

@Injectable()
export class ShrlDynamicLoaderService {
    constructor(
        @Inject(DynamicLoaderConfigurationToken)
        private readonly config: IDynamicLoaderConfiguration,
        private readonly injector: Injector,
        private readonly environmentInjector: EnvironmentInjector,
    ) {}

    public getDynamicModule(alias: string) {
        const moduleConfig = this.config.moduleConfigs[alias];

        if (!moduleConfig) return this.moduleConfigNotFound(alias);

        const module = moduleConfig.loadModule();
        const module$ = from(Promise.resolve(module));

        return module$;
    }

    // public getDynamicModuleEntity<T>(alias: string, token: ProviderToken<T>, injector: Injector = this.injector) {
    //     const dynamicRootComponent$ = this.getDynamicModule(alias, injector).pipe(
    //         map((moduleRef) => moduleRef.injector.get(token, undefined, InjectFlags.Optional)),
    //     );

    //     return dynamicRootComponent$;
    // }

    public getDynamicModuleRef(alias: string, injector: Injector = this.injector) {
        const moduleRefAndEntity$ = this.getDynamicModule(alias).pipe(
            map((module) => createNgModule(module, injector)),
        );

        return moduleRefAndEntity$;
    }

    // public getDynamicModuleRefAndEntity<T>(alias: string, token: ProviderToken<T>, injector: Injector = this.injector) {
    //     const moduleRefAndEntity$ = this.getDynamicModule(alias).pipe(
    //         map((module) => {
    //             return {
    //                 module,
    //                 injector: createEnvironmentInjector([], this.environmentInjector),
    //                 entity: moduleRef.injector.get(token),
    //             };
    //         }),
    //     );

    //     return moduleRefAndEntity$;
    // }

    public getDynamicModuleRefAndComponent(alias: string, injector: Injector = this.injector) {
        const moduleRefAndEntity$ = this.getDynamicModule(alias).pipe(
            map((module) => {
                return {
                    module,
                    injector: injector,
                    entity: (module as any)?.entryComponent,
                };
            }),
        );

        return moduleRefAndEntity$;
    }

    public isModuleConfigExists(alias: string) {
        return !!this.config.moduleConfigs[alias];
    }

    private moduleConfigNotFound(alias: string) {
        if (this.config.devMode) {
            console.warn(
                `Could not find module config with alias: ${alias}. Did you mean one from these ${Object.keys(
                    this.config.moduleConfigs,
                ).join('\r\t')}`,
            );
        }

        return throwError(() => `There are no module config with alias: ${alias}`);
    }
}
