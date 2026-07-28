import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { GpllGameDefinitionsListModule } from '@itdl/game-portal/features/game-definition';

@Component({
    selector: 'gpll-game-defs-content',
    templateUrl: './game-defs-content.component.html',
    styleUrls: ['./game-defs-content.component.scss'],
    standalone: true,
    imports: [CommonModule, GpllGameDefinitionsListModule],
})
export class GpllGameDefsContentComponent {}
