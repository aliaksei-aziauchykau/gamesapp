import { Injectable } from '@angular/core';
import { Dictionary, isNotNullOrUndefined, RxHubConnection } from '@itdl/shared/common';
import { BehaviorSubject, filter, map, Observable, tap, withLatestFrom } from 'rxjs';

import {
    getStaticTypeProperty,
    IUiActionService,
    UiAction,
    UiActionExecution,
    UiActionExecutionInfo,
    UiActionHolder,
    UiActionMetaInfo,
} from '../models/models';
import { processAction } from '../rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ShrlUiActionsService implements IUiActionService {
    private readonly _actions$: BehaviorSubject<Dictionary<string, UiActionHolder>> = new BehaviorSubject({});

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    public action$<T>(actionType: { new (...args: any[]): T }): BehaviorSubject<UiActionMetaInfo<T> | null> {
        const type = getStaticTypeProperty(actionType);
        const currentHolder = this.getCurrentHolder(type);

        return currentHolder.action;
    }

    public dispatch<T>(action: T): Observable<UiActionExecution<T>> {
        const type = getStaticTypeProperty(action);
        const currentHolder = this.getCurrentHolder(type);

        const uiActionMetaInfo = new UiActionMetaInfo(action, this);
        uiActionMetaInfo.executionInfo.next(new UiActionExecutionInfo(action));

        currentHolder.action.next(uiActionMetaInfo);

        return uiActionMetaInfo.executionInfo.pipe(isNotNullOrUndefined());
    }

    public dispatchAndWaitCompletion<T>(action: T): Observable<UiActionExecution<T>> {
        return this.dispatch(action).pipe(filter((execution) => execution.status === 'Completed'));
    }

    public processAction<T>(actionMetaInfo: UiActionMetaInfo<T>): void {
        const type = getStaticTypeProperty(actionMetaInfo.action);
        const currentHolder = this.getCurrentHolder(type);

        const currentExecutionInfo = actionMetaInfo.executionInfo.value;
        if (!currentExecutionInfo || currentExecutionInfo.status !== 'Dispatched') return;

        actionMetaInfo.executionInfo.next(new UiActionExecutionInfo(actionMetaInfo.action, 'Completed'));

        currentHolder.action.next(null);
    }

    public getSocketActionHandler =
        (connection$: Observable<RxHubConnection>) =>
        <T>(action: UiAction<T>, handler: (pair: [action: T, hub: RxHubConnection]) => void) =>
            this.action$(action).pipe(
                isNotNullOrUndefined(),
                withLatestFrom(connection$),
                tap(([{ action }, hub]) => handler([action, hub])),
                map(([action]) => action),
                processAction(),
            );

    private getCurrentHolder(type: string) {
        let currentHolder = this._actions$.value[type];

        if (!currentHolder) {
            currentHolder = new UiActionHolder();
            this._actions$.value[type] = currentHolder;
            this._actions$.next(this._actions$.value);
        }

        return currentHolder;
    }
}
