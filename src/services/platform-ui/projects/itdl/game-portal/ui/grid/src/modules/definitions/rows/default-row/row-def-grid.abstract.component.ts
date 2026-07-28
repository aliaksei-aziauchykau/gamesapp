import { Component, inject, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatRowDef, MatTable } from '@angular/material/table';
import { RowDefGridConfig } from '../../../../models/grid-config.model';

@Component({ selector: '', template: '' })
export abstract class RowDefGridComponent implements OnInit, OnDestroy {
    @Input() rowDefConfig!: RowDefGridConfig;
    @ViewChild(MatRowDef, { static: true }) rowDef!: MatRowDef<unknown>;

    private readonly table = inject(MatTable<unknown>, { optional: true });

    ngOnInit(): void {
        if (!this.table) throw new Error('RowGrid must be used within a <table mat-table> element');
        this.rowDef;
        this.table.addRowDef(this.rowDef);
    }

    ngOnDestroy(): void {
        if (!this.table) throw new Error('RowGrid must be used within a <table mat-table> element');

        this.table.removeRowDef(this.rowDef);
    }
}
