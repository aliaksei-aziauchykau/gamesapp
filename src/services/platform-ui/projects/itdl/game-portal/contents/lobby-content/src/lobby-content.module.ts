import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllRunningGameContainerComponent } from '@itdl/game-portal/features/game-container';
import { ShrlDynamicLoaderModule } from '@itdl/shared/dynamic-loader';
import { RxLet } from '@rx-angular/template/let';

import { GpllLobbyContentComponent } from './lobby-content.component';
import { lobbyContentLazyModulesConfig } from './lobby-content.lazy-modules';

@NgModule({
    imports: [
        CommonModule,
        GpllRunningGameContainerComponent,
        RxLet,
        ShrlDynamicLoaderModule.configure(lobbyContentLazyModulesConfig),
    ],
    exports: [GpllLobbyContentComponent],
    declarations: [GpllLobbyContentComponent],
    providers: [],
})
export class GpllLobbyContentModule {}
