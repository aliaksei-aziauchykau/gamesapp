import { Injector, Type } from '@angular/core';

export interface IDynamicStandaloneRenderConfiguration {
    loadModule: () => Promise<Type<unknown>>;
    inputs?: Record<string, unknown>;
    injector?: Injector;
    providersFn?: (injector: Injector) => Injector;
}
