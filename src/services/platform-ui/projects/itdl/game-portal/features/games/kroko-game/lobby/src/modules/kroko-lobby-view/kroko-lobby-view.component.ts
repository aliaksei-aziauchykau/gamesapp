import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { GpllKrokoGameActionsService, GpllKrokoPlayerListComponent } from '@itdl/game-portal/features/games/kroko-game';
import { IKrokoGameSettings } from '@itdl/game-portal/features/games/kroko-game/entities';
import { KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';

import { GpllKrokoGameSettingsComponent } from '../kroko-game-settings/kroko-game-settings.component';

type State = {
    gameSettings: IKrokoGameSettings;
};

@Component({
    selector: 'gpll-kroko-lobby-view',
    templateUrl: './kroko-lobby-view.component.html',
    styleUrls: ['./kroko-lobby-view.component.scss'],
    standalone: true,
    imports: [CommonModule, GpllKrokoPlayerListComponent, GpllKrokoGameSettingsComponent, RxLet],
    providers: [GpllKrokoGameActionsService, RxState],
})
export class GpllKrokoLobbyViewComponent implements OnInit {
    private readonly actionService = inject(GpllKrokoGameActionsService);
    private readonly store = inject(Store);

    private readonly gameCode = this.store.selectSnapshot(GameXSState.gameCode);
    private readonly userId = this.store.selectSnapshot(UserXSState.userIdSafety);

    public gameSettings$ = this.state.select('gameSettings');

    private readonly getGameContext = this.actionService.getGameContextActionFn(this.gameCode);
    private readonly getGameSettings = this.actionService.getGameSettingsActionFn(this.gameCode);
    private readonly joinPlayer = this.actionService.getJoinPlayerActionFn(this.gameCode, this.userId);

    constructor(private readonly state: RxState<State>) {
        this.state.connect('gameSettings', this.store.select(KrokoGameXSState.gameSettings));
    }

    ngOnInit(): void {
        this.getGameContext();
        this.getGameSettings();
        this.joinPlayer('AliakseiLobby');
    }
}
