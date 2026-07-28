import { Injectable } from '@angular/core';
import { isNotNullOrUndefined, ReactiveValue, RxHubConnection } from '@itdl/shared/common';

@Injectable()
export class ShrlSocketConnectionProvider extends ReactiveValue<RxHubConnection> {
    constructor() {
        super(null);
    }

    public connection$ = this.value$.pipe(isNotNullOrUndefined());

    public closeConnection() {
        this.value?.stop();
        this.next(null);
    }
}
