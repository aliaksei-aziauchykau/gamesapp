import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { RxHubConnection } from '@itdl/shared/common';
import { RxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import { tap } from 'rxjs';

import { ShrlSignalrService } from '../signalr.service';
import { ShrlSocketConnectionProvider } from './socket-connection.provider';

@Component({
    selector: 'shrl-socket-connection',
    templateUrl: 'socket-connection.component.html',
    imports: [RxLet, CommonModule],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [RxState],
})
export class ShrlSocketConnectionComponent implements OnInit {
    @Input() path!: string;

    @ContentChild(TemplateRef) content!: TemplateRef<unknown>;

    private readonly socketConnectionProvider = inject(ShrlSocketConnectionProvider);
    private readonly shrlSignalrService = inject(ShrlSignalrService);

    constructor(private readonly state: RxState<{ connection: RxHubConnection }>) {}

    public readonly connection$ = this.state
        .select('connection')
        .pipe(tap((connection) => console.log('refddddr', connection)));

    ngOnInit(): void {
        this.state.hold(this.shrlSignalrService.createRxHubConnectionAndStart(this.path), (connection) =>
            this.socketConnectionProvider.next(connection),
        );
        this.state.connect('connection', this.socketConnectionProvider.connection$);
    }
}
