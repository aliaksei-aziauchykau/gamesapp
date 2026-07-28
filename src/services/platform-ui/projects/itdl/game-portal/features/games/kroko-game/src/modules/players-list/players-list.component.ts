import { Component, Injector, OnDestroy, OnInit } from '@angular/core';
import { DestroyableComponent, IInjectorProvider, ServerSideDataEMPTY } from '@itdl/shared/common';
import { ShrlSignalrService } from '@itdl/shared/signalr';
import { switchMap, takeUntil } from 'rxjs';

@Component({
    selector: 'gpll-players-list',
    templateUrl: './players-list.component.html',
    styleUrls: ['./players-list.component.scss'],
})
export class GpllPlayersListComponent extends DestroyableComponent implements OnInit, OnDestroy, IInjectorProvider {
    constructor(private readonly signalrService: ShrlSignalrService, public readonly injector: Injector) {
        super();
    }

    ngOnInit(): void {
        // this.getPlayers$().subscribe((data) => console.log(data));
    }

    @ServerSideDataEMPTY()
    private getPlayers$() {
        return this.signalrService.createRxHubConnectionAndStart('chatHub').pipe(
            switchMap((hub) => hub.on('PlayerListUpdated')),
            takeUntil(this.onDestroy),
        );
    }
}
