import { CommonModule } from '@angular/common';
import { Component, ContentChild, inject, TemplateRef } from '@angular/core';
import { ShrlSocketConnectionComponent, ShrlSocketConnectionProvider } from '@itdl/shared/signalr';
import { RxState } from '@rx-angular/state';

import { ShrlServerLoaderModule } from '@itdl/shared/ui';
import { GpllKrokoGameSocketState } from './kroko-game.socket.state';

@Component({
    selector: 'gpll-kroko-game-socket',
    templateUrl: './kroko-game.socket.component.html',
    imports: [ShrlSocketConnectionComponent, CommonModule, ShrlServerLoaderModule],
    providers: [GpllKrokoGameSocketState, ShrlSocketConnectionProvider, RxState],
    standalone: true,
})
export class GpllKrokoGameSocketComponent {
    private readonly _ = inject(GpllKrokoGameSocketState);
    @ContentChild(TemplateRef) content!: TemplateRef<unknown>;
}
