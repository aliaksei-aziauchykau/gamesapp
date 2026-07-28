import { inject, InjectionToken, TransferState } from '@angular/core';

import { serverDataStateKey } from '../server-data/server-data.token';
import { IClientData } from './client-data.interface';

export const ClientDataToken = new InjectionToken<IClientData>('IClientDate', {
    factory: () => {
        const transferState = inject(TransferState);
        const dataFormServer = transferState.get<IClientData>(serverDataStateKey, {} as IClientData);
        return dataFormServer;
    },
});
