import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllRunningGameContainerComponent } from '@itdl/game-portal/features/game-container';
import { GpllGameContainerSocketComponent } from '@itdl/game-portal/features/game-container/socket';
import { DrawToolStateXSState, KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { GpllKrokoGameSocketComponent } from '@itdl/game-portal/features/games/kroko-game/socket';
import { PlayerXSState } from '@itdl/game-portal/features/player/ng-xs';
import { ShrlSocketConnectionComponent } from '@itdl/shared/signalr';
import { NgxsModule } from '@ngxs/store';
import { RxLet } from '@rx-angular/template/let';

import { GpllKrokoGameComponent } from './kroko-game.component';
import { GpllKrokoGameViewModule } from './modules/kroko-game-view/kroko-game-view.module';
import { GpllKrokoGameActionsService } from './services/kroko-game.actions.service';

@NgModule({
    declarations: [GpllKrokoGameComponent],
    exports: [GpllKrokoGameComponent],
    imports: [
        CommonModule,
        GpllGameContainerSocketComponent,
        GpllKrokoGameSocketComponent,
        GpllKrokoGameViewModule,
        GpllRunningGameContainerComponent,
        RxLet,
        NgxsModule.forFeature([KrokoGameXSState, DrawToolStateXSState, PlayerXSState]),
        ShrlSocketConnectionComponent,
    ],
    providers: [GpllKrokoGameActionsService],
})
export class GpllKrokoGameModule {
    public static readonly entryComponent = GpllKrokoGameComponent;
}
