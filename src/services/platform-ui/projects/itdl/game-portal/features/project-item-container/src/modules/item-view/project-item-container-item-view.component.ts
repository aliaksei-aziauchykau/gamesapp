import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ProjectItemContainer } from '@itdl/game-portal/features/project-item-container/entities';
import { GpllEventItemViewComponent } from '@itdl/game-portal/features/project-item-container/items/events';
import { GpllPostItemViewComponent } from '@itdl/game-portal/features/project-item-container/items/posts';
import { ShrlForeachModule } from '@itdl/shared/common';
import { GpllProjectItemContainerItemFetcherComponent } from '../item-fetcher/project-item-container-item-fetcher.component';

@Component({
    selector: 'gpll-project-item-container-item-view',
    templateUrl: './project-item-container-item-view.component.html',
    styleUrl: './project-item-container-item-view.component.scss',
    standalone: true,
    imports: [
        CommonModule,
        ShrlForeachModule,
        GpllProjectItemContainerItemFetcherComponent,
        GpllPostItemViewComponent,
        GpllEventItemViewComponent,
    ],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllProjectItemContainerItemViewComponent {
    @Input() public item!: ProjectItemContainer;
}
