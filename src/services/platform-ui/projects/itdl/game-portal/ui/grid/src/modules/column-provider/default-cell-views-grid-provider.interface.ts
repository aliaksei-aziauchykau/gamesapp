import { CellView } from '../cell-views/default-data/cell-view.abstract.component';
import { ICellViewsGridProvider } from './cell-views-grid-provider.interface';

export class DefaultCellViewsGridProvider implements ICellViewsGridProvider {
    private readonly columnMap = new Map<unknown, CellView[]>();

    public addCell(dataRow: unknown, columnGrid: CellView): void {
        this.columnMap.has(dataRow)
            ? this.columnMap.get(dataRow)?.push(columnGrid)
            : this.columnMap.set(dataRow, [columnGrid]);
    }

    public removeCell(dataRow: unknown, columnGrid: CellView): void {
        if (!this.columnMap.has(dataRow)) return;

        const columns = this.columnMap.get(dataRow)!;
        this.columnMap.delete(dataRow);
        const columnsFiltered = columns.filter((x) => x !== columnGrid);

        if (columnsFiltered.length === columns.length) {
            throw new Error('Column not found');
        }

        const columnsToSet = columns.filter((x) => x !== columnGrid);

        if (columnsToSet.length === 0) {
            this.columnMap.delete(dataRow);
            return;
        }

        this.columnMap.set(dataRow, columnsToSet);
    }

    public getCells(dataRow: unknown): CellView[] | undefined {
        return this.columnMap.get(dataRow);
    }

    public getCellsEnsure(dataRow: unknown): CellView[] {
        const columns = this.getCells(dataRow);

        if (!columns) {
            throw new Error('Columns not found');
        }

        return columns;
    }

    public getCellsEnsureAs<T extends CellView>(dataRow: unknown, type: { new (): T }): T[] {
        const columns = this.getCellsEnsure(dataRow);

        const typed = columns.filter((x) => x instanceof type).map((x) => x as T);
        return typed;
    }
}
