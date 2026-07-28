import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class ShrlFormDataSyncNotificationService {
    private _isFormSyncRequired = new BehaviorSubject(void 0);

    public isFormSyncRequired$ = this._isFormSyncRequired.asObservable();

    public markAsRequiredToSync() {
        this._isFormSyncRequired.next(void 0);
    }
}
