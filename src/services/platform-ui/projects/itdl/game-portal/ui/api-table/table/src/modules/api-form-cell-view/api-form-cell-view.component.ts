import { CommonModule } from '@angular/common';
import { Component, ElementRef, inject, Input, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';
import {
    DefaultApiFormSubmitActionService,
    GpllApiFormModule,
    GpllApiFormSubmitActionServiceToken,
    SubmitApiFormActionResult,
} from '@itdl/game-portal/ui/api-form/form';
import { ApiFormStoreActions } from '@itdl/game-portal/ui/api-form/ng-xs';
import { CellView } from '@itdl/game-portal/ui/grid';
import { detailExpand } from '@itdl/shared/animations';
import { catchAny, ReactiveValue } from '@itdl/shared/common';
import { Store } from '@ngxs/store';
import { rxState } from '@rx-angular/state';
import { rxEffects } from '@rx-angular/state/effects';
import { RxLet } from '@rx-angular/template/let';
import { delay, switchMap, tap } from 'rxjs';
import { GpllApiTableComponent } from '../../api-table.component';
import { GpllApiTableRefService } from '../../services/api-table-ref.service';
type State = {
    expandedElement: { id: any } | null;
    submitApiFormActionResult: SubmitApiFormActionResult;
    panelState: PanelState;
};

@Component({
    selector: '',
    templateUrl: './api-form-cell-view.component.html',
    styleUrls: ['./api-form-cell-view.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule, GpllApiFormModule, RxLet],
    animations: [detailExpand],
    providers: [
        DefaultApiFormSubmitActionService,
        { provide: GpllApiFormSubmitActionServiceToken, useExisting: DefaultApiFormSubmitActionService },
    ],
})
export class GpllApiFormCellViewComponent extends CellView implements OnInit {
    @Input() displayedColumnLength!: string;
    @Input() schema!: IFormEntitySchema;

    private readonly apiTableRefService = inject(GpllApiTableRefService);
    private readonly apiFormSubmitActionService = inject(GpllApiFormSubmitActionServiceToken);
    private readonly elementRef = inject(ElementRef);
    private readonly store = inject(Store);

    private tableExpandedElement = this.apiTableRefService.valueNotNull$.pipe(
        switchMap((table: GpllApiTableComponent) => table.state.select('expandedElement')),
    );
    private readonly state = rxState<State>(({ connect, set }) => {
        set({ expandedElement: null, panelState: new PanelState() });
        connect('expandedElement', this.tableExpandedElement);
        connect('submitApiFormActionResult', this.apiFormSubmitActionService.valueNotNull$);
    });

    readonly expandedElement$ = this.state
        .select('expandedElement')
        .pipe(tap((x) => console.log('expandedElement$', x)));

    readonly panelState$ = this.state.select('panelState').pipe(switchMap((x) => x.value$));

    private submitAction$ = this.state.select('submitApiFormActionResult').pipe(
        switchMap(({ model, formValue, apiForm }) => {
            apiForm.showSpinner();
            return this.store.dispatch(new ApiFormStoreActions.UpdateEntity({ ...model, ...formValue })).pipe(
                delay(2000),
                catchAny(() => apiForm.hideSpinner()),
            );
        }),
    );

    private readonly _effects = rxEffects(({ register }) => {
        register(this.submitAction$);
    });

    public override ngOnInit(): void {
        super.ngOnInit();
        const element = this.elementRef.nativeElement as HTMLElement;
        element.closest('td')?.setAttribute('colspan', this.displayedColumnLength);
    }

    public toggleApiFormPanel() {
        const panelState = this.state.get('panelState');
        panelState.set(panelState.value === 'expanded' ? 'collapsed' : 'expanded');
    }
}

class PanelState extends ReactiveValue<'expanded' | 'collapsed'> {
    constructor() {
        super('collapsed');
    }
}
