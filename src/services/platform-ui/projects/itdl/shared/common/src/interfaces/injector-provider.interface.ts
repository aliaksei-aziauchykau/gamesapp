import { Injector } from '@angular/core';

export interface IInjectorProvider {
    injector: Injector;
}

export const isInjectorProvider = (obj: any): obj is IInjectorProvider => {
    return obj && obj.injector !== undefined;
};
