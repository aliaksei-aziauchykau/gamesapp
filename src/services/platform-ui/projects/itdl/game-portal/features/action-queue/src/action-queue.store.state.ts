/* eslint-disable sort-exports/sort-exports */
import { Injectable } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import { Action, State, StateContext } from '@ngxs/store';
import { rxState } from '@rx-angular/state';
import { concatMap, delay, Observable, Subject } from 'rxjs';

import { ActionQueueStoreActions } from './action-queue.store.actions';

export interface IActionQueueXSState {
    queue: [];
}

const defaults: IActionQueueXSState = {
    queue: [],
};

@State<IActionQueueXSState>({
    name: StorageKeys.ActionQueueState,
    defaults,
})
@Injectable()
export class ActionQueueXSState {
    private readonly queue = new Subject<() => Observable<any>>();

    private readonly execution$ = this.queue.asObservable().pipe(concatMap((fn) => fn()));

    private readonly _state = rxState(({ connect }) => connect(this.execution$));

    @Action(ActionQueueStoreActions.AddToQueue)
    public async addToQueue(
        { dispatch }: StateContext<IActionQueueXSState>,
        { action }: ActionQueueStoreActions.AddToQueue,
    ) {
        this.queue.next(() => dispatch(action).pipe(delay(2000)));
    }
}
