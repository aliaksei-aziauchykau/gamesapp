import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChild, inject, TemplateRef } from '@angular/core';
import { GpllProjectItemContainerApiService } from '@itdl/game-portal/features/project-item-container/api';
import { ProjectItemContainer } from '@itdl/game-portal/features/project-item-container/entities';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import buildQuery from 'odata-query';

type State = {
    items: ProjectItemContainer[];
};

@Component({
    selector: 'gpll-project-item-container-list-fetcher',
    templateUrl: './project-item-container-list-fetcher.component.html',
    styleUrl: './project-item-container-list-fetcher.component.scss',
    standalone: true,
    imports: [CommonModule, RxLet],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [GpllProjectItemContainerApiService],
})
export class GpllProjectItemContainerListFetcherComponent {
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;
    private readonly apiService = inject(GpllProjectItemContainerApiService);
    private readonly state = rxState<State>(({ connect }) => {
        connect('items', this.apiService.getAllOData(buildQuery({ expand: `Posts,Events` })));
    });
    readonly items$ = this.state.select('items');
}
