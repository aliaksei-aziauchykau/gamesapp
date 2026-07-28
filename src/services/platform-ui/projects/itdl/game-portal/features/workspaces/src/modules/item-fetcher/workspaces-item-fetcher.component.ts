import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { GpllWorkspacesApiService } from '@itdl/game-portal/features/workspaces/api';
import { Workspace } from '@itdl/game-portal/features/workspaces/entities';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import buildQuery from 'odata-query';

type State = {
    workspace: Workspace;
};

@Component({
    selector: 'gpll-workspaces-item-fetcher',
    templateUrl: './workspaces-item-fetcher.component.html',
    styleUrl: './workspaces-item-fetcher.component.scss',
    standalone: true,
    imports: [CommonModule, RxLet],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [GpllWorkspacesApiService],
})
export class GpllWorkspacesItemFetcherComponent implements OnInit {
    @Input() public query: string = '';
    @Input() public id!: string;
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;
    private readonly apiService = inject(GpllWorkspacesApiService);
    private readonly state = rxState<State>();
    readonly workspace$ = this.state.select('workspace');

    ngOnInit(): void {
        this.state.connect(
            'workspace',
            this.apiService.getByIdOData(this.id, this.query ?? buildQuery()),
        );
    }
}
