import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { CellView } from '@itdl/game-portal/ui/grid';
import { rxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import { switchMap } from 'rxjs';
import { GpllApiTableComponent } from '../../api-table.component';
import { GpllApiTableRefService } from '../../services/api-table-ref.service';
import { GpllApiFormCellViewComponent } from '../api-form-cell-view/api-form-cell-view.component';

@Component({
    selector: '',
    templateUrl: './api-table-actions-cell-view.component.html',
    styleUrls: ['./api-table-actions-cell-view.component.scss'],
    standalone: true,
    imports: [CommonModule, MatTableModule, MatIconModule, MatButtonModule, RxLet],
    host: {},
})
export class GpllApiTableActionsCellViewComponent extends CellView implements OnInit {
    private readonly apiTableRefService = inject(GpllApiTableRefService);
    private apiFormCellViewComponent!: GpllApiFormCellViewComponent;

    private tableExpandedElement = this.apiTableRefService.valueNotNull$.pipe(
        switchMap((table: GpllApiTableComponent) => table.state.select('expandedElement')),
    );
    private readonly state = rxState<{ expandedElement: { id: any } | null }>(({ connect }) =>
        connect('expandedElement', this.tableExpandedElement),
    );

    readonly expandedElement$ = this.state.select('expandedElement');

    public toggleApiFormPanel() {
        const apiFormCell = this.cellViewsGridProvider.getCellsEnsureAs(this.dataRow, GpllApiFormCellViewComponent)[0];
        apiFormCell.toggleApiFormPanel();
    }

    public override ngOnInit(): void {
        super.ngOnInit();
    }
}
