import { Component, inject, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { IControlEntitySchema, IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';
import { GpllApiFormSubmitActionServiceToken } from '@itdl/game-portal/ui/api-form/form';

import { GpllGridConfigBuilder, GridConfig } from '@itdl/game-portal/ui/grid';
import { detailExpand } from '@itdl/shared/animations';
import { Store } from '@ngxs/store';
import { rxState } from '@rx-angular/state';
import { GpllApiFormCellViewComponent } from './modules/api-form-cell-view/api-form-cell-view.component';
import { GpllApiFormRowDefGridComponent } from './modules/api-form-row-def/api-form-row-def.component';
import { GpllApiTableActionsCellViewComponent } from './modules/api-table-actions-cell-view/api-table-actions-cell-view.component';
import { GpllApiTableRefService } from './services/api-table-ref.service';

type State = {
    expandedElement: { id: any } | null;
};

@Component({
    selector: 'gpll-api-table',
    templateUrl: './api-table.component.html',
    styleUrls: ['./api-table.component.scss'],
    animations: [detailExpand],
    providers: [GpllApiTableRefService],
    encapsulation: ViewEncapsulation.None,
})
export class GpllApiTableComponent implements OnInit, OnDestroy {
    @Input() entities: unknown[] = [];
    @Input() schema!: IFormEntitySchema;

    private readonly apiFormSubmitActionService = inject(GpllApiFormSubmitActionServiceToken);
    private readonly apiTableRefService = inject(GpllApiTableRefService);
    private readonly store = inject(Store);

    readonly state = rxState<State>(() => ({ expandedElement: null }));

    public schemas: IControlEntitySchema[] = [];
    public displayedColumns: string[] = [];
    public config!: GridConfig;

    ngOnInit(): void {
        this.schemas = Object.values(this.schema.controls) as IControlEntitySchema[];
        this.displayedColumns = this.schemas.map((schema) => schema.name);
        this.displayedColumns.push('actions');
        this.apiTableRefService.next(this);
        this.config = this.buildGridConfig();
        this.state.set({ expandedElement: null });
    }

    ngOnDestroy(): void {
        this.apiTableRefService.next(null);
    }

    public buildGridConfig() {
        var builder = new GpllGridConfigBuilder();
        builder
            .withDefaultHeaderRowDef(this.displayedColumns)
            .withDefaultRowDef(this.displayedColumns)
            .withRowDef({
                displayedColumns: ['apiForm'],
                viewConfig: {
                    component: GpllApiFormRowDefGridComponent,
                },
            })
            .withDefaultColumnDef('apiForm')
            .withCellViewComponent({
                inputs: { schema: this.schema, displayedColumnLength: this.displayedColumns.length },
                component: GpllApiFormCellViewComponent,
            })
            .complete()
            .withDefaultColumnDef('actions')
            .withDefaultHeaderCellViewComponent('Actions')
            .withCellViewComponent({
                component: GpllApiTableActionsCellViewComponent,
            });

        this.schemas.forEach((schema) => builder.withDefaultColumn(schema.name));

        console.log(builder.config);

        return builder.config;
    }
}
