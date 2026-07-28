import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { GpllLoaderComponent } from '@itdl/game-portal/ui';
import { GpllCardModule } from '@itdl/game-portal/ui/card';
import { ShrlServerLoaderModule } from '@itdl/shared/ui';
import { RxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';

import { GpllJoinPlayerDialogModule } from '../join-player-dialog/join-player-dialog.module';
import { GpllKrokoBoardComponent } from '../kroko-board/kroko-board.component';
import { GpllKrokoBoardToolsModule } from '../kroko-board/module/kroko-board-tools/kroko-board-tools.module';
import { GpllDrawToolConfigService } from '../kroko-board/services/draw-tool-config.service';
import { GpllDrawToolStageService } from '../kroko-board/services/draw-tool-stage.service';
import { GpllDrawToolStateService } from '../kroko-board/services/draw-tool-state.service';
import { GpllKrokoChatComponent } from '../kroko-chat/kroko-chat.component';
import { GpllKrokoPlayerListComponent } from '../kroko-player-list/kroko-player-list.component';
import { GpllPlayersListModule } from '../players-list/players-list.module';
import { GpllSelectWordDialogModule } from '../select-word-dialog/select-word-dialog.module';
import { GpllKrokoGameViewComponent } from './kroko-game-view.component';

@NgModule({
    declarations: [GpllKrokoGameViewComponent],
    exports: [GpllKrokoGameViewComponent],
    imports: [
        CommonModule,
        GpllCardModule,
        GpllJoinPlayerDialogModule,
        GpllKrokoBoardComponent,
        GpllKrokoBoardToolsModule,
        GpllKrokoChatComponent,
        GpllKrokoPlayerListComponent,
        GpllPlayersListModule,
        GpllSelectWordDialogModule,
        MatButtonModule,
        MatCardModule,
        MatIconModule,
        RxLet,
        ShrlServerLoaderModule.withLoaderComponent(GpllLoaderComponent),
        // ShrlServerLoaderModule.withProviders([
        //     {
        //         provide: ShrlServerLoaderComponentTypeToken,
        //         useValue: GpllLoaderComponent,
        //     },
        // ]),
    ],
    providers: [
        GpllDrawToolConfigService,
        GpllDrawToolStageService,
        GpllDrawToolStateService,
        RxState,
        // ShrlSocketConnectionProvider,
    ],
})
export class GpllKrokoGameViewModule {}
