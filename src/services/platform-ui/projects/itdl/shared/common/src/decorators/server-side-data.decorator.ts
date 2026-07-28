import { InjectionToken } from '@angular/core';
import { EMPTY, Observable, of } from 'rxjs';

import { isInjectorProvider } from '../interfaces/injector-provider.interface';
import { isServerPlatformToken } from '../tokens/is-server-platform.token';

type Selector<T, K> = (dataProvider: T) => K;
type Map<T> = (data: T) => T | Observable<T>;

export function ServerSideDataByToken<T, K>(type: InjectionToken<T>, selector: Selector<T, K>, map?: Map<K>) {
    return function (target: unknown, methodName: string, descriptor: PropertyDescriptor) {
        const original: (...args: unknown[]) => Observable<unknown> = descriptor.value;

        descriptor.value = function (...args: unknown[]) {
            if (!isInjectorProvider(this))
                throw Error(
                    'ServerSideDataByToken decorator can be used only for components which implements InjectorProvider interface',
                );

            const injector = this.injector;
            const isServerPlatform = injector.get(isServerPlatformToken);
            const dataProvider = injector.get(type);

            if (isServerPlatform && dataProvider) {
                const data = selector(dataProvider);

                if (!data) return original.apply(this, args);

                return (map && map(data)) || data;
            }

            return original.apply(this, args);
        };

        return descriptor;
    };
}

export function ServerSideData<T>(data: T) {
    return function (target: unknown, methodName: string, descriptor: PropertyDescriptor) {
        const original: (...args: unknown[]) => Observable<unknown> = descriptor.value;

        descriptor.value = function (...args: unknown[]) {
            if (!isInjectorProvider(this))
                throw Error(
                    'ServerSideSkip decorator can be used only for components which implements InjectorProvider interface',
                );

            const injector = this.injector;
            const isServerPlatform = injector.get(isServerPlatformToken);

            if (isServerPlatform) return data;

            return original.apply(this, args);
        };

        return descriptor;
    };
}

export const ServerSideDataEMPTY = () => ServerSideData(EMPTY);

export const ServerSideDataEmptyArray = () => ServerSideData(of([]));
