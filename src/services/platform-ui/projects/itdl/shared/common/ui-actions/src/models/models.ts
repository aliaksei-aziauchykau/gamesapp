import { BehaviorSubject, Observable } from 'rxjs';

export interface IUiActionService {
    dispatch<T>(action: T): Observable<UiActionExecution<T>>;
    action$<T>(actionType: { new (...args: any[]): T }): BehaviorSubject<UiActionMetaInfo<T> | null>;
    processAction<T>(actionMetaInfo: UiActionMetaInfo<T>): void;
}

export type UiAction<T> = { new (...args: any[]): T };

export type UiActionExecution<T> = {
    action: T;
    status: 'Dispatched' | 'Completed';
};

export class UiActionExecutionInfo<T = any> {
    action!: T;
    status!: 'Dispatched' | 'Completed';

    constructor(action: T, status: 'Dispatched' | 'Completed' = 'Dispatched') {
        this.action = action;
        this.status = status;
    }
}

export class UiActionHolder {
    action = new BehaviorSubject<UiActionMetaInfo | null>(null);
}

export class UiActionMetaInfo<T = any> {
    action!: T;
    service!: IUiActionService;
    executionInfo = new BehaviorSubject<UiActionExecutionInfo<T> | null>(null);

    constructor(action: T, service: IUiActionService) {
        this.action = action;
        this.service = service;
    }
}

export function getStaticTypeProperty(obj: any) {
    if (isDefinitionHasStaticTypeProperty(obj)) return obj.type;

    if (isInstanceHasStaticTypeProperty(obj)) return obj.constructor.type;

    throw Error('obj has not static type property. Neither in instance or definition');
}

export function isDefinitionHasStaticTypeProperty(obj: any): obj is { type: string } {
    return 'type' in obj;
}

export function isHasStaticTypeProperty(obj: any): boolean {
    return isInstanceHasStaticTypeProperty(obj) || isDefinitionHasStaticTypeProperty(obj);
}

export function isInstanceHasStaticTypeProperty(obj: any): obj is { constructor: { type: string } } {
    return 'type' in obj.constructor;
}
