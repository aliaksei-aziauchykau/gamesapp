import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';
import { GameDefinitionXSState } from '@itdl/game-portal/features/game-definition/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { tap } from 'rxjs';

@Component({
    selector: 'gpll-lobby-content',
    templateUrl: './lobby-content.component.html',
    styleUrls: ['./lobby-content.component.scss'],
    providers: [RxState],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllLobbyContentComponent {
    public readonly gameDefinition$ = this.state
        .select('gameDefinition')
        .pipe(tap((aa) => console.log('gameDefinition', aa)));

    constructor(private readonly store: Store, private readonly state: RxState<{ gameDefinition: GameDefinition }>) {
        this.state.connect(
            'gameDefinition',
            this.store.select(GameDefinitionXSState.currentGameDefinition).pipe(isNotNullOrUndefined()),
        );
    }
}
