import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GpllGameModule } from '../../game.module';
import { GpllGameItemModule } from '../game-item/game-item.module';
import { GpllGamesListItemDirective } from './directives/games-list-item.directive';
import { GpllGamesListComponent } from './games-list.component';

@NgModule({
    imports: [CommonModule, GpllGameItemModule, GpllGameModule],
    declarations: [GpllGamesListComponent, GpllGamesListItemDirective],
    exports: [GpllGamesListComponent, GpllGamesListItemDirective],
})
export class GpllGamesListModule {}
