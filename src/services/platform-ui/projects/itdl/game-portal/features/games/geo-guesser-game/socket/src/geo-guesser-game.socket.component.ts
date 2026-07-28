import { CommonModule } from '@angular/common';
import { Component, ContentChild, inject, TemplateRef } from '@angular/core';
import { ShrlSocketConnectionComponent, ShrlSocketConnectionProvider } from '@itdl/shared/signalr';
import { RxState } from '@rx-angular/state';

import { GpllGeoGuesserGameSocketState } from './geo-guesser-game.socket.state';

@Component({
    selector: 'gpll-geo-guesser-game-socket',
    templateUrl: './geo-guesser-game.socket.component.html',
    imports: [ShrlSocketConnectionComponent, CommonModule],
    providers: [GpllGeoGuesserGameSocketState, ShrlSocketConnectionProvider, RxState],
    standalone: true,
})
export class GpllGeoGuesserGameSocketComponent {
    private readonly _ = inject(GpllGeoGuesserGameSocketState);
    @ContentChild(TemplateRef) content!: TemplateRef<unknown>;
}
