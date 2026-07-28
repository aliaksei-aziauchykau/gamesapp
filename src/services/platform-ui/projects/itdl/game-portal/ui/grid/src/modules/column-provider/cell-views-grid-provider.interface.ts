import { CellView } from '../cell-views/default-data/cell-view.abstract.component';

export interface ICellViewsGridProvider {
    addCell(dataRow: unknown, columnGrid: CellView): void;
    removeCell(dataRow: unknown, columnGrid: CellView): void;
    getCells(dataRow: unknown): CellView[] | undefined;
    getCellsEnsure(dataRow: unknown): CellView[];
    getCellsEnsureAs<T extends CellView>(dataRow: unknown, type: { new (): T }): T[];
}
