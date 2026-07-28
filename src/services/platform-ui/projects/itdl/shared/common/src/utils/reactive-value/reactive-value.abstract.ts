import { BehaviorSubject } from 'rxjs';

import { isNotNullOrUndefined } from '../rxjs/guards.utils';
import { IReactiveValue } from './reactive-value.interface';

export abstract class ReactiveValue<T> implements IReactiveValue<T> {
    private readonly _value$!: BehaviorSubject<T | null>;

    constructor(initialValue: T | null = null) {
        this._value$ = new BehaviorSubject<T | null>(initialValue);
    }

    public get value() {
        return this._value$.value;
    }

    public get valueSafe() {
        if (this.value === null) throw new Error('ReactiveValue is null');
        return this.value;
    }

    public get value$() {
        return this._value$.asObservable();
    }

    public get valueNotNull$() {
        return this._value$.asObservable().pipe(isNotNullOrUndefined());
    }

    public next(value: T | null): void {
        this._value$.next(value);
    }

    public is(value: T | null): boolean {
        return this.value === value;
    }

    public set(value: T | null): void {
        this.next(value);
    }
}
