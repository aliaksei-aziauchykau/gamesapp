import { Component, inject, OnInit } from '@angular/core';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';
import { GameDefinitionXSState } from '@itdl/game-portal/features/game-definition/ng-xs';
import { KrokoGameStoreActions } from '@itdl/game-portal/features/games/kroko-game/actions';
import { PlayerStoreActions } from '@itdl/game-portal/features/player/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { rxState } from '@rx-angular/state';

type State = {
    gameDefinition: GameDefinition;
};
@Component({
    selector: 'gpll-kroko-game',
    templateUrl: './kroko-game.component.html',
    styleUrls: ['./kroko-game.component.scss'],
    standalone: false,
})
export class GpllKrokoGameComponent implements OnInit {
    private readonly store = inject(Store);
    private readonly state = rxState<State>(({ connect }) =>
        connect(
            'gameDefinition',
            this.store.select(GameDefinitionXSState.currentGameDefinition).pipe(isNotNullOrUndefined()),
        ),
    );

    public readonly gameDefinition$ = this.state.select('gameDefinition');

    // public readonly playlist: Track[] = [
    //     {
    //         title: 'Audio Title',
    //         link: '/assets/audio/track1.mp3',
    //         artist: 'Artist',
    //         duration: 200,
    //     },
    // ];

    ngOnInit(): void {
        this.store.dispatch(new KrokoGameStoreActions.CleanGameContext());
        this.store.dispatch(new PlayerStoreActions.GeneratePlayerNickname());
    }
}
