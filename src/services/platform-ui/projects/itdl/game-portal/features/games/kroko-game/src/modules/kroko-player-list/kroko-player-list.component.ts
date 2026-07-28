import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { IKrokoPlayer } from '@itdl/game-portal/features/games/kroko-game/entities';
import { KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { GpllChangePlayerDialogModule, GpllChangePlayerDialogService } from '@itdl/game-portal/features/player';
import { PlayerStoreActions, PlayerXSState } from '@itdl/game-portal/features/player/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { rxActions } from '@rx-angular/state/actions';
import { RxLet } from '@rx-angular/template/let';
import { map, switchMap, tap } from 'rxjs';

type State = {
    players: IKrokoPlayer[];
    userId: string;
};

@Component({
    selector: 'gpll-kroko-player-list',
    templateUrl: './kroko-player-list.component.html',
    styleUrls: ['./kroko-player-list.component.scss'],
    imports: [
        CommonModule,
        MatCardModule,
        RxLet,
        GpllTranslationModule,
        MatButtonModule,
        MatIconModule,
        GpllChangePlayerDialogModule,
    ],
    providers: [RxState],
    standalone: true,
})
export class GpllKrokoPlayerListComponent {
    public readonly players$ = this.state.select('players');
    public readonly userId$ = this.state.select('userId');

    public readonly rxUiActions = rxActions<{ changePlayer: void }>();
    public readonly uiActions = inject(ShrlUiActionsService);

    private readonly changePlayerDialogService = inject(GpllChangePlayerDialogService);
    private readonly store = inject(Store);

    private readonly changePlayerEffects$ = this.rxUiActions.changePlayer$.pipe(
        map(() => this.store.selectSnapshot(PlayerXSState.player)),
        map((player) => this.changePlayerDialogService.openDialog(player)),
        switchMap((dialogRef) => dialogRef.afterClosed()),
        isNotNullOrUndefined(),
        map((player) => player),
        isNotNullOrUndefined(),
        tap((player) => this.store.dispatch(new PlayerStoreActions.StorePlayer(player))),
    );

    constructor(private readonly state: RxState<State>) {
        this.state.connect('players', this.store.select(KrokoGameXSState.players));
        this.state.connect('userId', this.store.select(UserXSState.userId).pipe(isNotNullOrUndefined()));
        this.state.hold(this.changePlayerEffects$);
    }
}
