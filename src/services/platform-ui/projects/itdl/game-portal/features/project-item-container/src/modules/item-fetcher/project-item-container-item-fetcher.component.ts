import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { GpllProjectItemContainerApiService } from '@itdl/game-portal/features/project-item-container/api';
import { ProjectItemContainer } from '@itdl/game-portal/features/project-item-container/entities';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import buildQuery from 'odata-query';

type State = {
    item: ProjectItemContainer;
};

@Component({
    selector: 'gpll-project-item-container-item-fetcher',
    templateUrl: './project-item-container-item-fetcher.component.html',
    styleUrl: './project-item-container-item-fetcher.component.scss',
    standalone: true,
    imports: [CommonModule, RxLet],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [GpllProjectItemContainerApiService],
})
export class GpllProjectItemContainerItemFetcherComponent implements OnInit {
    @Input() public query: string = '';
    @Input() public id!: string;
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;
    private readonly apiService = inject(GpllProjectItemContainerApiService);
    private readonly state = rxState<State>();
    readonly item$ = this.state.select('item');

    ngOnInit(): void {
        this.state.connect(
            'item',
            this.apiService.getByIdOData(this.id, this.query ?? buildQuery({ expand: `Posts,Events` })),
        );
    }
}
