import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Event } from '@itdl/game-portal/features/project-item-container/entities';

@Component({
    selector: 'gpll-event-item-view',
    templateUrl: './event-item-view.component.html',
    styleUrls: ['./event-item-view.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [MatCardModule, CommonModule],
    standalone: true,
})
export class GpllEventItemViewComponent {
    @Input() public event!: Event;
}
