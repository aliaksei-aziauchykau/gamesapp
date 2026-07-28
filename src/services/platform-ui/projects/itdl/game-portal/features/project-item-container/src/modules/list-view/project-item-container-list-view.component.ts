import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ProjectItemContainer } from '@itdl/game-portal/features/project-item-container/entities';
import { ShrlForeachModule } from '@itdl/shared/common';
import { GpllProjectItemContainerItemViewComponent } from '../item-view/project-item-container-item-view.component';

@Component({
    selector: 'gpll-project-item-container-list-view',
    templateUrl: './project-item-container-list-view.component.html',
    styleUrl: './project-item-container-list-view.component.scss',
    standalone: true,
    imports: [CommonModule, ShrlForeachModule, GpllProjectItemContainerItemViewComponent],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllProjectItemContainerListViewComponent {
    @Input() public items!: ProjectItemContainer[];
}
