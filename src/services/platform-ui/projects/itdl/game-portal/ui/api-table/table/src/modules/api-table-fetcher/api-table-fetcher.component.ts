import { CommonModule } from '@angular/common';
import { Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { CollectionNameType } from '@itdl/game-portal/ui/api-form/entities';
import { ApiTableStoreActions, ApiTableXSState } from '@itdl/game-portal/ui/api-table/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';

type State = {
    entities: unknown[];
};

@Component({
    selector: 'gpll-api-table-fetcher',
    standalone: true,
    imports: [CommonModule, RxLet],
    templateUrl: './api-table-fetcher.component.html',
    styleUrls: ['./api-table-fetcher.component.scss'],
})
export class GpllApiTableFetcherComponent implements OnInit {
    @Input() public collectionName!: CollectionNameType;
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;

    private readonly store = inject(Store);
    private readonly state = rxState<State>();
    public readonly entities$ = this.state.select('entities');

    ngOnInit(): void {
        this.state.connect(
            'entities',
            this.store.select(ApiTableXSState.entities(this.collectionName)).pipe(isNotNullOrUndefined()),
        );
        this.store.dispatch(new ApiTableStoreActions.FetchEntities(this.collectionName));
    }
}
