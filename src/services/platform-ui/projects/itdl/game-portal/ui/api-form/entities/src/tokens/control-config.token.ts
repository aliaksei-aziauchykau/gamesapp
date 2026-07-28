import { InjectionToken } from '@angular/core';

import { IControlEntityConfig } from '../interfaces/control-entity-config.interface';

export const GpllControlConfigsToken = new InjectionToken<IControlEntityConfig[]>('Gpll control config tokens');
// {
//     factory: () => [],
// }
