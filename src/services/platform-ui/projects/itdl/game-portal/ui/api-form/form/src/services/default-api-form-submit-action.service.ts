import { Injectable } from '@angular/core';

import { ReactiveValue } from '@itdl/shared/common';
import { IApiFormSubmitActionService } from '../models/api-form-submit-action.service.interface';
import { SubmitApiFormActionResult } from '../models/submit-action.model';

@Injectable({ providedIn: 'root' })
export class DefaultApiFormSubmitActionService
    extends ReactiveValue<SubmitApiFormActionResult>
    implements IApiFormSubmitActionService
{
    constructor() {
        super();
    }
}
