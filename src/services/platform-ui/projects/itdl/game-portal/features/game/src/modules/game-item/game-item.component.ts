import { Component, inject, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Game } from '@itdl/game-portal/features/game/entities';

@Component({
    selector: 'gpll-game-item',
    templateUrl: './game-item.component.html',
    styleUrls: ['./game-item.component.scss'],
})
export class GpllGameItemComponent {
    @Input() game!: Game;

    private readonly router = inject(Router);

    public navigateToGame() {
        this.router.navigate(['game', this.game.code]);
    }

    public navigateToLobby() {
        this.router.navigate(['lobby', this.game.code]);
    }
}
