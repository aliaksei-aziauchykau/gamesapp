import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';
import { GameDefinitionXSState } from '@itdl/game-portal/features/game-definition/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';

@Component({
    selector: 'gpll-game-content',
    templateUrl: './game-content.component.html',
    styleUrls: ['./game-content.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllGameContentComponent {
    public readonly gameDefinition$ = this.state.select('gameDefinition');

    constructor(private readonly store: Store, private readonly state: RxState<{ gameDefinition: GameDefinition }>) {
        this.state.connect(
            'gameDefinition',
            this.store.select(GameDefinitionXSState.currentGameDefinition).pipe(isNotNullOrUndefined()),
        );
    }
}
