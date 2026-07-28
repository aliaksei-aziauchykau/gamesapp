import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';

import { isInjectorProvider } from '../../interfaces/injector-provider.interface';
import { isServerPlatformToken } from '../../tokens/is-server-platform.token';

type Selector<T, K> = (dataProvider: T) => K;
type Map<T> = (data: T) => T | Observable<T>;

export function ClientSideDataByToken<T, K>(type: InjectionToken<T>, selector: Selector<T, K>, map?: Map<K>) {
    return function (target: unknown, methodName: string, descriptor: PropertyDescriptor) {
        const original: (...args: unknown[]) => Observable<unknown> = descriptor.value;

        descriptor.value = function (...args: unknown[]) {
            if (!isInjectorProvider(this))
                throw Error(
                    'ClientSideDataByToken decorator can be used only for components which implements InjectorProvider interface',
                );

            const injector = this.injector;
            const isServerPlatform = injector.get(isServerPlatformToken);
            const dataProvider = injector.get(type);

            if (!isServerPlatform && dataProvider) {
                const data = selector(dataProvider);

                if (!data) return original.apply(this, args);

                return (map && map(data)) || data;
            }

            return original.apply(this, args);
        };

        return descriptor;
    };
}
