import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';
import { GameDefinitionXSState } from '@itdl/game-portal/features/game-definition/ng-xs';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';

@Component({
    selector: 'gpll-game-definitions-list',
    templateUrl: './game-definitions-list.component.html',
    styleUrls: ['./game-definitions-list.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllGameDefinitionsListComponent {
    @Select(GameDefinitionXSState.gameDefinitions)
    public readonly gameDefinitions$!: Observable<GameDefinition[]>;
}
