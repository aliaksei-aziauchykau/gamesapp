import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { GpllWorkspacesApiService } from '@itdl/game-portal/features/workspaces/api';
import { Workspace } from '@itdl/game-portal/features/workspaces/entities';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import buildQuery from 'odata-query';

type State = {
    workspaces: Workspace[];
};

@Component({
    selector: 'gpll-workspaces-list-fetcher',
    templateUrl: './workspaces-list-fetcher.component.html',
    styleUrl: './workspaces-list-fetcher.component.scss',
    standalone: true,
    imports: [CommonModule, RxLet],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [GpllWorkspacesApiService],
})
export class GpllWorkspacesListFetcherComponent implements OnInit {
    @Input() public query: string = '';
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;
    private readonly apiService = inject(GpllWorkspacesApiService);
    private readonly state = rxState<State>();
    readonly workspaces$ = this.state.select('workspaces');

    ngOnInit(): void {
        this.state.connect('workspaces', this.apiService.getAllOData(this.query ?? buildQuery()));
    }
}
