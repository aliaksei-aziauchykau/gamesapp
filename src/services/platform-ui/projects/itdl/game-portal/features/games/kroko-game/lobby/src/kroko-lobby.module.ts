import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { GpllKrokoGameSocketComponent } from '@itdl/game-portal/features/games/kroko-game/socket';
import { ShrlSocketConnectionComponent, ShrlSocketConnectionProvider } from '@itdl/shared/signalr';
import { NgxsModule } from '@ngxs/store';

import { GpllKrokoLobbyComponent } from './kroko-lobby.component';
import { GpllKrokoLobbyViewComponent } from './modules/kroko-lobby-view/kroko-lobby-view.component';

@NgModule({
    declarations: [GpllKrokoLobbyComponent],
    exports: [GpllKrokoLobbyComponent],
    imports: [
        CommonModule,
        GpllKrokoGameSocketComponent,
        GpllKrokoLobbyViewComponent,

        NgxsModule.forFeature([KrokoGameXSState]),
        ShrlSocketConnectionComponent,
    ],
    providers: [ShrlSocketConnectionProvider],
})
export class GpllKrokoLobbyModule {
    public static readonly entryComponent = GpllKrokoLobbyComponent;
}
