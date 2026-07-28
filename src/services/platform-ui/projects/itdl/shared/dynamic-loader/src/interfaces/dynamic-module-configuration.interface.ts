import { Type } from '@angular/core';

export interface IDynamicModuleConfiguration {
    loadModule: () => Promise<Type<unknown>>;
}
