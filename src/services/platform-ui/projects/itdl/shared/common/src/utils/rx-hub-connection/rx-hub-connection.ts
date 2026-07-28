import { Injector } from '@angular/core';
import { HubConnection, HubConnectionState } from '@microsoft/signalr';
import { from, map, Observable } from 'rxjs';

import { ServerSideDataEMPTY } from '../../decorators/server-side-data.decorator';

export class RxHubConnection {
    constructor(private readonly source: HubConnection, private readonly injector: Injector) {}

    public get state(): HubConnectionState {
        return this.source.state;
    }

    public get connectionId() {
        return this.source.connectionId;
    }

    @ServerSideDataEMPTY()
    public stop(): Observable<void> {
        return from(this.source.stop());
    }

    @ServerSideDataEMPTY()
    public start(): Observable<RxHubConnection> {
        return from(this.source.start()).pipe(map(() => this));
    }

    @ServerSideDataEMPTY()
    public invoke<T>(methodName: string, ...args: unknown[]): Observable<T> {
        return from(this.source.invoke(methodName, ...args));
    }

    @ServerSideDataEMPTY()
    public send(methodName: string, ...args: unknown[]): Observable<void> {
        return from(this.source.send(methodName, ...args));
    }

    @ServerSideDataEMPTY()
    public on<T>(methodName: string): Observable<T> {
        return new Observable<T>((observer) => {
            const handler = function (data: T) {
                observer.next(data);
            };
            this.source.on(methodName, handler);

            return () => {
                console.log('Cinoe!');

                this.source.off(methodName, handler);

                return observer.unsubscribe();
            };
        });
    }

    @ServerSideDataEMPTY()
    public onReconnected$(): Observable<unknown> {
        return new Observable((observer) => {
            this.source.onreconnected(() => observer.next({}));

            return () => {
                return observer.unsubscribe();
            };
        });
    }

    @ServerSideDataEMPTY()
    public onReconnecting$(): Observable<unknown> {
        return new Observable((observer) => {
            this.source.onreconnecting(() => observer.next({}));

            return () => {
                return observer.unsubscribe();
            };
        });
    }
}
