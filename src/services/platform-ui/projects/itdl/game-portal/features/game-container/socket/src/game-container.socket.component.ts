import { CommonModule } from '@angular/common';
import { Component, ContentChild, inject, TemplateRef } from '@angular/core';
import { ShrlSocketConnectionComponent, ShrlSocketConnectionProvider } from '@itdl/shared/signalr';
import { RxState } from '@rx-angular/state';

import { GpllGameContainerSocketState } from './game-container.socket.state';

@Component({
    selector: 'gpll-game-container-socket',
    templateUrl: './game-container.socket.component.html',
    imports: [ShrlSocketConnectionComponent, CommonModule],
    providers: [GpllGameContainerSocketState, ShrlSocketConnectionProvider, RxState],
    standalone: true,
})
export class GpllGameContainerSocketComponent {
    private readonly _ = inject(GpllGameContainerSocketState);
    @ContentChild(TemplateRef) content!: TemplateRef<unknown>;
}
