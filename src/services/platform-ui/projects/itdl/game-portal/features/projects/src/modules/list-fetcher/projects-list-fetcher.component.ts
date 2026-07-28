import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { GpllProjectsApiService } from '@itdl/game-portal/features/projects/api';
import { Project } from '@itdl/game-portal/features/projects/entities';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import buildQuery from 'odata-query';

type State = {
    projects: Project[];
};

@Component({
    selector: 'gpll-projects-list-fetcher',
    templateUrl: './projects-list-fetcher.component.html',
    styleUrl: './projects-list-fetcher.component.scss',
    standalone: true,
    imports: [CommonModule, RxLet],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [GpllProjectsApiService],
})
export class GpllProjectsListFetcherComponent implements OnInit {
    @Input() public query: string = '';
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;
    private readonly apiService = inject(GpllProjectsApiService);
    private readonly state = rxState<State>();
    readonly projects$ = this.state.select('projects');

    ngOnInit(): void {
        this.state.connect('projects', this.apiService.getAllOData(this.query ?? buildQuery()));
    }
}
