import { Directive, inject, Input, OnInit } from '@angular/core';
import { RxState } from '@rx-angular/state';

import { ShrlSignalrService } from '../signalr.service';
import { ShrlSocketConnectionProvider } from './socket-connection.provider';

@Directive({ selector: '[shrlSocketConnection]', standalone: true, providers: [RxState] })
export class ShrlSocketConnectionDirective implements OnInit {
    @Input() shrlSocketConnection!: string;

    private readonly socketConnectionProvider = inject(ShrlSocketConnectionProvider);
    private readonly shrlSignalrService = inject(ShrlSignalrService);
    private readonly state = inject(RxState);

    ngOnInit(): void {
        this.state.hold(
            this.shrlSignalrService.createRxHubConnectionAndStart(this.shrlSocketConnection),
            (connection) => this.socketConnectionProvider.next(connection),
        );
    }
}
