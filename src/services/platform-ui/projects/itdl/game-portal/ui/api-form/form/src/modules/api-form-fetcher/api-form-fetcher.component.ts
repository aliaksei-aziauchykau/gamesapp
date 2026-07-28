import { CommonModule } from '@angular/common';
import { Component, ContentChild, inject, Input, OnInit, TemplateRef } from '@angular/core';
import { CollectionNameType, IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';
import { ApiFormStoreActions, ApiFormXSState } from '@itdl/game-portal/ui/api-form/ng-xs';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';

type State = {
    uiSchema: IFormEntitySchema;
};

@Component({
    selector: 'gpll-api-form-fetcher',
    standalone: true,
    imports: [CommonModule, RxLet],
    templateUrl: './api-form-fetcher.component.html',
    styleUrls: ['./api-form-fetcher.component.scss'],
})
export class GpllApiFormFetcherComponent implements OnInit {
    @Input() public collectionName!: CollectionNameType;
    @ContentChild(TemplateRef, { static: true }) public content!: TemplateRef<unknown>;

    private readonly store = inject(Store);
    private readonly state = rxState<State>();
    public readonly uiSchema$ = this.state.select('uiSchema');

    ngOnInit(): void {
        this.state.connect(
            'uiSchema',
            this.store.select(ApiFormXSState.uiSchema(this.collectionName)).pipe(isNotNullOrUndefined()),
        );
        this.store.dispatch(new ApiFormStoreActions.FetchUiSchema(this.collectionName));
    }
}
