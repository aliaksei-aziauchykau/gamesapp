import { Component, Input, OnInit } from '@angular/core';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';

@Component({
    selector: 'gpll-game-definition-item',
    templateUrl: './game-definition-item.component.html',
    styleUrls: ['./game-definition-item.component.scss'],
})
export class GpllGameDefinitionItemComponent implements OnInit {
    @Input() gameDefinition!: GameDefinition;

    public route!: string;

    ngOnInit(): void {
        this.route = `${this.gameDefinition.name}/games`;
    }
}
