import { Observable } from 'rxjs';

export interface IReactiveValue<T> {
    value: T | null;
    valueSafe: T;
    value$: Observable<T | null>;
    valueNotNull$: Observable<T>;
    next(value: T | null): void;
}
