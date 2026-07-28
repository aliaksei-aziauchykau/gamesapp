import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, HostListener, inject, OnDestroy, OnInit } from '@angular/core';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { IGeoGuesserGameContext } from '@itdl/game-portal/features/games/geo-guesser-game/entities';
import { GpllGeoGuesserGameXSState } from '@itdl/game-portal/features/games/geo-guesser-game/ng-xs';
import { PlayerXSState } from '@itdl/game-portal/features/player/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { fade } from '@itdl/shared/animations';
import { Store } from '@ngxs/store';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';

import { GpllGeoGuesserGameActionsService } from '../../services/geo-guesser-game-actions.service';
import { GpllGeoGuesserGameBoardComponent } from '../geo-guesser-game-board/geo-guesser-game-board.component';
import { GeoGuesserPlayerListComponent } from '../geo-guesser-player-list/geo-guesser-player-list.component';

type State = {
    gameContext: IGeoGuesserGameContext;
};

@Component({
    selector: 'gpll-geo-guesser-game-view',
    standalone: true,
    templateUrl: './geo-guesser-game-view.component.html',
    styleUrls: ['./geo-guesser-game-view.component.scss'],
    imports: [CommonModule, GpllGeoGuesserGameBoardComponent, GeoGuesserPlayerListComponent, RxLet],
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [fade],
})
export class GpllGeoGuesserGameViewComponent implements OnInit, OnDestroy {
    private readonly actions = inject(GpllGeoGuesserGameActionsService);
    private readonly store = inject(Store);
    private readonly state = rxState<State>(({ connect }) => {
        connect('gameContext', this.store.select(GpllGeoGuesserGameXSState.gameContext));
    });

    private readonly gameCode = this.store.selectSnapshot(GameXSState.gameCode);
    private readonly userId = this.store.selectSnapshot(UserXSState.userIdSafety);
    private readonly nickname = this.store.selectSnapshot(PlayerXSState.playerNickname);

    public readonly gameContext$ = this.state.select('gameContext');

    public readonly fetchGameContext = this.actions.getGameContextActionFn(this.gameCode);
    public readonly leavePlayer = this.actions.getLeavePlayerActionFn(this.gameCode, this.userId);
    public readonly joinPlayer = this.actions.getJoinPlayerActionFn(this.gameCode, this.userId);

    public ngOnInit(): void {
        this.fetchGameContext();
        this.joinPlayer(this.nickname);
    }

    @HostListener('window:beforeunload', ['$event'])
    public ngOnDestroy(): void {
        this.leavePlayer();
    }
}
