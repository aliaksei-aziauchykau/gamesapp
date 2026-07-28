import { Component, ContentChild, Input, TemplateRef } from '@angular/core';
import { Game } from '@itdl/game-portal/features/game/entities';

import { GpllGamesListItemDirective } from './directives/games-list-item.directive';

@Component({
    selector: 'gpll-games-list',
    templateUrl: './games-list.component.html',
    styleUrls: ['./games-list.component.scss'],
})
export class GpllGamesListComponent {
    @Input() public games: Game[] = [];
    @ContentChild(GpllGamesListItemDirective) public gamesListItemTemplate!: TemplateRef<unknown>;
}
