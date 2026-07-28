import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllRunningGameContainerComponent } from '@itdl/game-portal/features/game-container';
import { GpllGameContainerSocketComponent } from '@itdl/game-portal/features/game-container/socket';
import { GpllGeoGuesserGameXSState } from '@itdl/game-portal/features/games/geo-guesser-game/ng-xs';
import { GpllGeoGuesserGameSocketComponent } from '@itdl/game-portal/features/games/geo-guesser-game/socket';
import { PlayerXSState } from '@itdl/game-portal/features/player/ng-xs';
import { ShrlSocketConnectionComponent } from '@itdl/shared/signalr';
import { NgxsModule } from '@ngxs/store';
import { RxLet } from '@rx-angular/template/let';

import { GpllGeoGuesserGameComponent } from './geo-guesser-game.component';
import { GpllGeoGuesserGameViewComponent } from './modules/geo-guesser-game-view/geo-guesser-game-view.component';
import { GpllGeoGuesserGameActionsService } from './services/geo-guesser-game-actions.service';

@NgModule({
    declarations: [GpllGeoGuesserGameComponent],
    exports: [GpllGeoGuesserGameComponent],
    imports: [
        CommonModule,
        GpllGameContainerSocketComponent,
        GpllGeoGuesserGameSocketComponent,
        GpllGeoGuesserGameViewComponent,
        GpllRunningGameContainerComponent,
        RxLet,
        NgxsModule.forFeature([GpllGeoGuesserGameXSState, PlayerXSState]),
        ShrlSocketConnectionComponent,
    ],
    providers: [GpllGeoGuesserGameActionsService],
})
export class GpllGeoGuesserGameModule {
    public static readonly entryComponent = GpllGeoGuesserGameComponent;
}
