import { Injector, Provider, StaticProvider } from '@angular/core';

export interface IDynamicRenderConfiguration {
    alias: string;
    inputs?: Record<string, unknown>;
    injector?: Injector;
    providersFn?: (injector: Injector) => Array<Provider | StaticProvider>;
}
