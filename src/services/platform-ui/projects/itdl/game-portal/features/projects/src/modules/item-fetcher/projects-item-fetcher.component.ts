import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { GpllProjectsApiService } from '@itdl/game-portal/features/projects/api';
import { Project } from '@itdl/game-portal/features/projects/entities';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import buildQuery from 'odata-query';

type State = {
    project: Project;
};

@Component({
    selector: 'gpll-projects-item-fetcher',
    templateUrl: './projects-item-fetcher.component.html',
    styleUrl: './projects-item-fetcher.component.scss',
    standalone: true,
    imports: [CommonModule, RxLet],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [GpllProjectsApiService],
})
export class GpllProjectsItemFetcherComponent implements OnInit {
    @Input() public query: string = '';
    @Input() public id!: string;
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;
    private readonly apiService = inject(GpllProjectsApiService);
    private readonly state = rxState<State>();
    readonly project$ = this.state.select('project');

    ngOnInit(): void {
        this.state.connect('project', this.apiService.getByIdOData(this.id, this.query ?? buildQuery()));
    }
}
