import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { GpllCreateGameButtonModule, GpllGameItemModule } from '@itdl/game-portal/features/game';
import { Game } from '@itdl/game-portal/features/game/entities';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';
import { GameDefinitionXSState } from '@itdl/game-portal/features/game-definition/ng-xs';
import { ShrlForeachModule } from '@itdl/shared/common';
import { Select } from '@ngxs/store';
import { RxLet } from '@rx-angular/template/let';
import { Observable } from 'rxjs';

@Component({
    selector: 'gpll-games-content',
    templateUrl: './games-content.component.html',
    styleUrls: ['./games-content.component.scss'],
    standalone: true,
    imports: [CommonModule, GpllCreateGameButtonModule, ShrlForeachModule, GpllGameItemModule, RxLet],
})
export class GpllGamesContentComponent {
    @Select(GameDefinitionXSState.currentGameDefinition)
    public readonly gameDefinition$!: Observable<GameDefinition>;

    @Select(GameXSState.games)
    public readonly games$!: Observable<Game[]>;

    // get by name games
}
