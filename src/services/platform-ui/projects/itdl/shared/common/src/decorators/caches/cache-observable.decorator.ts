/* Inspired by https://github.com/gurov/cache-observable */

import { EMPTY, Observable, timer } from 'rxjs';
import { first, shareReplay } from 'rxjs/operators';

/**
 * Cache for observable objects
 */
let cache: {
    [index: string]: Observable<any>;
} = {};

/**
 * Resets the cache.
 * Should be used for testing purposes only.
 */
export function resetCache() {
    cache = {};
}

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

/**
 * Deletes an entry from the cache by key
 */
export function cleanUpCache(key: string) {
    delete cache[key];
}

/**
 * @param {number} expiresInMs Cache Observable function for ms milliseconds
 * @param {string} customKey Custom key for caching (override default hashing algorithm)
 * @todo Doesn't work with types in arguments - return the same hash for all methods with different types (if type os the only only difference)
 */
export function CacheObservable(customKey?: string, expiresInMs?: number) {
    return function (target: any, methodName: string, descriptor: PropertyDescriptor) {
        const original: (...args: any[]) => Observable<any> = descriptor.value;
        const targetName = target.constructor.name;

        descriptor.value = function (...args: any[]) {
            const key = customKey ? customKey : hashCode(`${targetName}:${methodName}:${JSON.stringify(args)}`);

            const entry = cache[key];

            if (entry) {
                return entry;
            }

            const expired = expiresInMs ? timer(expiresInMs).pipe(first()) : EMPTY;

            cache[key] = original.apply(this, args).pipe(shareReplay(1));

            expired.subscribe(() => cleanUpCache(key));

            return cache[key];
        };

        return descriptor;
    };
}
