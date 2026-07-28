import { Injectable } from '@angular/core';

import { ReactiveValue } from '@itdl/shared/common';
import { IApiFormCancelActionService } from '../models/api-form-cancel-action.service.interface';

@Injectable({ providedIn: 'root' })
export class DefaultApiFormCancelActionService extends ReactiveValue<void> implements IApiFormCancelActionService {
    constructor() {
        super();
    }
}
