import { Injector, Type } from '@angular/core';

export class GridConfig {
    public title: string = '';
    public columnsDefs: ColumnDefGridConfig[] = [];
    public headerRowDefs: HeaderRowDefGridConfig[] = [];
    public rowDefs: RowDefGridConfig[] = [];

    // public get viewComponentConfigs() {
    //     return [
    //         ...this.columns.map((x) => x.viewConfig),
    //         ...this.headerRows.map((x) => x.viewConfig),
    //         ...this.rows.map((x) => x.viewConfig),
    //     ];
    // }
}

export interface IViewComponentConfig {
    component: Type<unknown>;
    inputs?: Record<string, unknown>;
    injector?: Injector;
    providersFn?: (injector: Injector) => Injector;
}

export class ColumnDefGridConfig {
    public matColumnDef!: string;
    public childrenViewComponentConfigs: ChildrenViewComponentConfigs = new ChildrenViewComponentConfigs();
    public viewComponentConfig!: ViewComponentConfig;
}

export class ChildrenViewComponentConfigs {
    public cell!: ViewComponentConfig;
    public header?: ViewComponentConfig;
}

export class RowDefGridConfig {
    public displayedColumns: string[] = [];
    public viewConfig!: ViewComponentConfig;
}

export class HeaderRowDefGridConfig {
    public displayedColumns: string[] = [];
    public viewConfig!: ViewComponentConfig;
}

export class ViewComponentConfig implements IViewComponentConfig {
    public component!: Type<unknown>;
    public inputs?: Record<string, unknown> = {};
    public injector?: Injector;
}
