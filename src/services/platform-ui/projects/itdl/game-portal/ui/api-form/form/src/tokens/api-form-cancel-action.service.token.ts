import { inject, InjectionToken } from '@angular/core';

import { IApiFormCancelActionService } from '../models/api-form-cancel-action.service.interface';
import { DefaultApiFormCancelActionService } from '../services/default-api-form-cancel-action.service';

export const GpllApiFormCancelActionServiceToken = new InjectionToken<IApiFormCancelActionService>(
    'Gpll api form cancel action service tokens',
    { providedIn: 'root', factory: () => inject(DefaultApiFormCancelActionService) },
);
