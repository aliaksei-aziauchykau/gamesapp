import { isInjectorProvider } from '../interfaces/injector-provider.interface';
import { isServerPlatformToken } from '../tokens/is-server-platform.token';

export function ServerSideSkip() {
    return function (target: unknown, methodName: string, descriptor: PropertyDescriptor) {
        const original: (...args: unknown[]) => unknown = descriptor.value;

        descriptor.value = function (...args: unknown[]) {
            if (!isInjectorProvider(this))
                throw Error(
                    'ServerSideSkip decorator can be used only for components which implements InjectorProvider interface',
                );

            const injector = this.injector;
            const isServerPlatform = injector.get(isServerPlatformToken);

            if (isServerPlatform) return;

            return original.apply(this, args);
        };

        return descriptor;
    };
}
