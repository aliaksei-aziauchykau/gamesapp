import { CommonModule } from '@angular/common';
import { type OnInit, Component, ContentChild, inject, Input, TemplateRef } from '@angular/core';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { GameContainerUiActions } from '@itdl/game-portal/features/game-container/ui-actions';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { Store } from '@ngxs/store';

import { GpllGameContainerModule } from '../../game-container.module';

@Component({
    selector: 'gpll-running-game-container',
    templateUrl: './running-game-container.component.html',
    styleUrls: ['./running-game-container.component.scss'],
    imports: [CommonModule, GpllGameContainerModule],
    standalone: true,
})
export class GpllRunningGameContainerComponent implements OnInit {
    @ContentChild(TemplateRef) content!: TemplateRef<unknown>;

    private readonly uiActions = inject(ShrlUiActionsService);
    private readonly store = inject(Store);

    @Input() gameDefName!: string;

    private readonly gameCode = this.store.selectSnapshot(GameXSState.gameCode);

    private readonly runContainer = (gameDefName: string, gameId: string) =>
        this.uiActions.dispatch(new GameContainerUiActions.Run(gameDefName, gameId));

    ngOnInit(): void {
        this.runContainer(this.gameDefName, this.gameCode);
    }
}
