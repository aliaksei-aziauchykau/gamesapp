import { Component, inject, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatColumnDef, MatTable } from '@angular/material/table';
import { ColumnDefGridConfig } from '../../../../models/grid-config.model';

@Component({ selector: '', template: '' })
export abstract class ColumnDefGrid implements OnInit, OnDestroy {
    @Input() columnDefConfig!: ColumnDefGridConfig;
    @ViewChild(MatColumnDef, { static: true }) columnDef!: MatColumnDef;

    private readonly table = inject(MatTable<unknown>, { optional: true });

    ngOnInit(): void {
        if (!this.table) throw new Error('ColumnGrid must be used within a <table mat-table> element');
        this.table.addColumnDef(this.columnDef);
    }

    ngOnDestroy(): void {
        if (!this.table) throw new Error('ColumnGrid must be used within a <table mat-table> element');

        this.table.removeColumnDef(this.columnDef);
    }
}
