import { Component, inject, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatHeaderRowDef, MatTable } from '@angular/material/table';
import { HeaderRowDefGridConfig } from '../../../../models/grid-config.model';

@Component({ selector: '', template: '' })
export abstract class HeaderRowDefGridComponent implements OnInit, OnDestroy {
    @Input() headerRowDefConfig!: HeaderRowDefGridConfig;
    @ViewChild(MatHeaderRowDef, { static: true }) headerRowDef!: MatHeaderRowDef;

    private readonly table = inject(MatTable<unknown>, { optional: true });

    ngOnInit(): void {
        if (!this.table) throw new Error('HeaderRowGrid must be used within a <table mat-table> element');
        this.table.addHeaderRowDef(this.headerRowDef);
    }

    ngOnDestroy(): void {
        if (!this.table) throw new Error('HeaderRowGrid must be used within a <table mat-table> element');

        this.table.removeHeaderRowDef(this.headerRowDef);
    }
}
