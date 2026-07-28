/* Inspired by https://github.com/gurov/cache-observable */

/**
 * Calc hash of string
 * Source: https://stackoverflow.com/a/7616484
 */
const hashCode = function (s: string): string {
    let hash = 0;
    let chr: number;

    if (s.length === 0) {
        return hash.toString(36);
    }

    for (let i = 0; i < s.length; i++) {
        chr = s.charCodeAt(i);
        // tslint:disable:no-bitwise
        hash = (hash << 5) - hash + chr;
        hash |= 0;
    }

    return hash.toString(36);
};

export type Cache<T> = Map<string, T>;

/**
 * @param {number} expiresInMs Cache Observable function for ms milliseconds
 * @param {string} customKey Custom key for caching (override default hashing algorithm)
 * @todo Doesn't work with types in arguments - return the same hash for all methods with different types (if type os the only only difference)
 */
export function CacheDecoratorFactory<T>(cache: Cache<T>) {
    return function Cacheable(customKey?: string) {
        return function (target: any, methodName: string, descriptor: PropertyDescriptor) {
            const original: (...args: unknown[]) => T = descriptor.value;
            const targetName = target.constructor.name;

            descriptor.value = function (...args: unknown[]) {
                const key = customKey ? customKey : hashCode(`${targetName}:${methodName}:${JSON.stringify(args)}`);

                const entry = cache.get(key);

                if (entry) {
                    return entry;
                }

                const output = original.apply(this, args);

                cache.set(key, output);

                return output;
            };

            return descriptor;
        };
    };
}
