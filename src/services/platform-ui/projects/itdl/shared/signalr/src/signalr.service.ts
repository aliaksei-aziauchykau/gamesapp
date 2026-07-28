import { inject, Injectable, Injector } from '@angular/core';
import { RxHubConnection, RxHubConnectionCache, RxHubConnectionCacheable } from '@itdl/shared/common';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { ShrlSignalrConfigToken } from './signalr.token';

@Injectable()
export class ShrlSignalrService {
    private readonly token = inject(ShrlSignalrConfigToken);
    private readonly baseUrl = this.token.apiUrl;

    constructor(private readonly injector: Injector) {}

    @RxHubConnectionCacheable()
    public createRxHubConnectionCacheable(path: string) {
        return this.createRxHubConnection(path);
    }

    public createRxHubConnection(path: string) {
        const hubConnection = new HubConnectionBuilder()
            .withUrl(`${this.baseUrl}/${path}`)
            .withAutomaticReconnect()
            .build();

        const rxHubConnection = new RxHubConnection(hubConnection, this.injector);

        return rxHubConnection;
    }

    public createRxHubConnectionAndStartCacheable(path: string) {
        const rxHubConnection = this.createRxHubConnectionCacheable(path);

        if (rxHubConnection.state !== 'Connected') return rxHubConnection.start();

        return new BehaviorSubject<RxHubConnection>(rxHubConnection);
    }

    public createRxHubConnectionAndStart(path: string) {
        const rxHubConnection = this.createRxHubConnection(path);

        if (rxHubConnection.state !== 'Connected') return rxHubConnection.start();

        return new BehaviorSubject<RxHubConnection>(rxHubConnection);
    }

    public stopAllConnections() {
        RxHubConnectionCache.forEach((rxHubConnection) => rxHubConnection.stop());
    }
}
