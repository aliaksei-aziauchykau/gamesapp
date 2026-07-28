import { Injectable } from '@angular/core';
import {
    ChildrenViewComponentConfigs,
    ColumnDefGridConfig,
    GridConfig,
    HeaderRowDefGridConfig,
    RowDefGridConfig,
    ViewComponentConfig,
} from '../models/grid-config.model';
import { GpllDefaultDataCellViewComponent } from '../modules/cell-views/default-data/default-data-cell-view.component';
import { GpllDefaultHeaderCellViewComponent } from '../modules/cell-views/default-header/default-header-cell-view.component';
import { GpllDefaultColumnDefGridComponent } from '../modules/definitions/columns/default/default-column-def-grid.component';
import { GpllDefaultHeaderRowDefGridComponent } from '../modules/definitions/rows/default-header/default-header-row-def-grid.component';
import { GpllDefaultRowDefGridComponent } from '../modules/definitions/rows/default-row/default-row-def-grid.component';

@Injectable()
export class GpllGridConfigBuilder {
    private _config: GridConfig = new GridConfig();

    public create(): GpllGridConfigBuilder {
        this._config = new GridConfig();
        return this;
    }

    public title(title: string): GpllGridConfigBuilder {
        this._config.title = title;
        return this;
    }

    public withDefaultColumnDef(matColumnDefName: string): DefaultColumnDefGridConfigBuilder {
        const builder = new DefaultColumnDefGridConfigBuilder();
        builder.link(this);

        const columnDefConfig = builder.create().withDefaultColumnDef().withColumnDefName(matColumnDefName).config;

        this._config.columnsDefs.push(columnDefConfig);

        return builder;
    }

    public withDefaultColumn(name: string): GpllGridConfigBuilder {
        return this.withDefaultColumnDef(name)
            .withDefaultDataCellViewComponent(name)
            .withDefaultHeaderCellViewComponent(name.charAt(0).toUpperCase() + name.slice(1))
            .complete();
    }

    public withDefaultRowDef(displayedColumns: string[]): GpllGridConfigBuilder {
        return this.withRowDef({
            displayedColumns,
            viewConfig: { component: GpllDefaultRowDefGridComponent },
        });
    }

    public withDefaultHeaderRowDef(displayedColumns: string[]): GpllGridConfigBuilder {
        return this.withHeaderRowDef({
            displayedColumns,
            viewConfig: { component: GpllDefaultHeaderRowDefGridComponent },
        });
    }

    public withRowDef(rowDefConfig: RowDefGridConfig): GpllGridConfigBuilder {
        rowDefConfig.viewConfig.inputs = { ...rowDefConfig.viewConfig.inputs, rowDefConfig };
        this._config.rowDefs.push(rowDefConfig);
        return this;
    }

    public withHeaderRowDef(headerRowDefConfig: HeaderRowDefGridConfig): GpllGridConfigBuilder {
        headerRowDefConfig.viewConfig.inputs = { ...headerRowDefConfig.viewConfig.inputs, headerRowDefConfig };
        this._config.headerRowDefs.push(headerRowDefConfig);
        return this;
    }

    public get config(): GridConfig {
        return this._config;
    }
}

export class DefaultColumnDefGridConfigBuilder {
    private _config = new ColumnDefGridConfig();
    private gridConfigBuilder!: GpllGridConfigBuilder;

    public create() {
        this._config = new ColumnDefGridConfig();
        return this;
    }

    public link(builder: GpllGridConfigBuilder) {
        this.gridConfigBuilder = builder;
        return this;
    }

    public complete() {
        return this.gridConfigBuilder;
    }

    public withDefaultColumnDef() {
        this._config.viewComponentConfig = {
            component: GpllDefaultColumnDefGridComponent,
            inputs: { columnDefConfig: this._config },
        };
        return this;
    }

    public withColumnDefName(name: string) {
        this._config.matColumnDef = name;
        return this;
    }

    public withDefaultDataCellViewComponent(dataFieldPath: string) {
        this._config.childrenViewComponentConfigs.cell = {
            component: GpllDefaultDataCellViewComponent,
            inputs: {
                dataFieldPath,
            },
        };
        return this;
    }

    public withDefaultHeaderCellViewComponent(name: string) {
        this._config.childrenViewComponentConfigs.header = {
            component: GpllDefaultHeaderCellViewComponent,
            inputs: { name },
        };
        return this;
    }

    public withCellViewComponent(cellView: ViewComponentConfig) {
        this._config.childrenViewComponentConfigs.cell = cellView;
        return this;
    }

    public get config(): ColumnDefGridConfig {
        return this._config;
    }
}

export class DefaultColumnDefGridConfig {
    public matColumnDef!: string;
    public childrenViewComponentConfigs!: ChildrenViewComponentConfigs;
}

export class DefaultRowDefGridConfigBuilder {
    private _config = new ColumnDefGridConfig();

    public create() {
        this._config = new ColumnDefGridConfig();
        return this;
    }

    public withDefaultViewComponent() {
        this._config.viewComponentConfig = {
            component: GpllDefaultColumnDefGridComponent,
            inputs: { columnDef: this._config },
        };
        return this;
    }

    public withColumnDefName(name: string) {
        this._config.matColumnDef = name;
        return this;
    }

    public withChildrenViewComponentConfigs(childrenViewComponentConfigs: ChildrenViewComponentConfigs) {
        this._config.childrenViewComponentConfigs = childrenViewComponentConfigs;
        return this;
    }

    public get config(): ColumnDefGridConfig {
        return this._config;
    }
}
