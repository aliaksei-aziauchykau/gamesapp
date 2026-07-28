import { InjectionToken, makeStateKey } from '@angular/core';

import { IServerData } from './server-data.interface';

export const ServerDataToken = new InjectionToken<IServerData>('IDataFromServer');

export const serverDataStateKey = makeStateKey<IServerData>('dataFromServer');
