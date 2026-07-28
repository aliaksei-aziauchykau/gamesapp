import { ChangeDetectionStrategy, Component, inject, Input } from '@angular/core';
import { GameActions } from '@itdl/game-portal/features/game/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { RxEffects } from '@rx-angular/state/effects';
import { switchMap, tap } from 'rxjs';

import { GpllCreateGameDialogService } from '../create-game-dialog/create-game-dialog.service';

@Component({
    selector: 'gpll-create-game-button',
    templateUrl: './create-game-button.component.html',
    styleUrls: ['./create-game-button.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [RxEffects],
})
export class GpllCreateGameButtonComponent {
    @Input() gameDefinitionId!: number;

    private readonly createGameDialogService = inject(GpllCreateGameDialogService);
    private readonly rxEffects = inject(RxEffects);
    private readonly store = inject(Store);

    private readonly createGame$ = this.createGameDialogService.value$.pipe(
        isNotNullOrUndefined(),
        switchMap((dialog) => dialog.afterClosed()),
        isNotNullOrUndefined(),
        tap((dialogData) => this.store.dispatch(new GameActions.CreateGame(this.gameDefinitionId, dialogData.name))),
    );

    public openCreateGameDialog(): void {
        this.createGameDialogService.openDialog();
    }

    constructor() {
        this.rxEffects.register(this.createGame$);
    }
}
