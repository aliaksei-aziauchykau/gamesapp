import { inject, InjectionToken } from '@angular/core';

import { IApiFormSubmitActionService } from '../models/api-form-submit-action.service.interface';
import { DefaultApiFormSubmitActionService } from '../services/default-api-form-submit-action.service';

export const GpllApiFormSubmitActionServiceToken = new InjectionToken<IApiFormSubmitActionService>(
    'Gpll api form submit action service tokens',
    { providedIn: 'root', factory: () => inject(DefaultApiFormSubmitActionService) },
);
