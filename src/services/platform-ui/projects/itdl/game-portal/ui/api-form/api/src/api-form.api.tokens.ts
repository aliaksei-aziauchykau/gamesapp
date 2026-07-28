import { InjectionToken } from '@angular/core';
import { IEntityApiService } from '@itdl/game-portal/http';

export const EntityApiServiceToken = new InjectionToken<IEntityApiService<unknown, any>>('EntityApiServiceToken token');
export const UiSchemaUrlToken = new InjectionToken('UiSchemaUrlToken token');
