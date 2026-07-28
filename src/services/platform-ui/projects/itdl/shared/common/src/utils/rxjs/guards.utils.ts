import { filter, Observable } from 'rxjs';

export function isInputNotNullOrUndefined<T>(value: null | undefined | T): value is T {
    return value !== null && value !== undefined;
}

export function isNotNullOrUndefined<T>() {
    return (source$: Observable<null | undefined | T>) => source$.pipe(filter(isInputNotNullOrUndefined));
}
