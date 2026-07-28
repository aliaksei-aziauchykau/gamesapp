import { Directive, Input, OnInit } from '@angular/core';
import { GameContainerUiActions } from '@itdl/game-portal/features/game-container/ui-actions';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';

@Directive({
    selector: '[gpllCreateGameContainerAction]',
    standalone: true,
})
export class CreateGameContainerActionDirective implements OnInit {
    @Input() public readonly gameDefName!: string;
    @Input() public readonly gameId!: string;

    @Dispatch() createContainer = (gameDefName: string, gameId: string) =>
        new GameContainerUiActions.Run(gameDefName, gameId);

    ngOnInit(): void {
        this.createContainer(this.gameDefName, this.gameId);
    }
}
