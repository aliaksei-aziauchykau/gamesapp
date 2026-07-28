import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { IGeoGuesserPlayer } from '@itdl/game-portal/features/games/geo-guesser-game/entities';
import { GpllGeoGuesserGameXSState } from '@itdl/game-portal/features/games/geo-guesser-game/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import { tap } from 'rxjs';

type State = {
    players: IGeoGuesserPlayer[];
    userId: string;
};

@Component({
    selector: 'gpll-geo-guesser-player-list',
    templateUrl: './geo-guesser-player-list.component.html',
    styleUrls: ['./geo-guesser-player-list.component.scss'],
    imports: [CommonModule, MatCardModule, RxLet, GpllTranslationModule, MatButtonModule, MatIconModule],
    standalone: true,
})
export class GeoGuesserPlayerListComponent {
    private readonly store = inject(Store);
    private readonly state = rxState<State>(({ connect }) => {
        connect(
            'players',
            this.store.select(GpllGeoGuesserGameXSState.players).pipe(tap((pl) => console.log('players', pl))),
        );
        connect('userId', this.store.select(UserXSState.userId).pipe(isNotNullOrUndefined()));
    });

    public readonly players$ = this.state.select('players');
    public readonly userId$ = this.state.select('userId');
}
