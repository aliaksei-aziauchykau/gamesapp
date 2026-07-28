import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ShrlLayoutComponent } from '@itdl/shared/layout';
import { RxState } from '@rx-angular/state';

@Component({
    selector: 'jpl-main-layout',
    templateUrl: './main-layout.component.html',
    styleUrls: ['./main-layout.component.scss'],
    standalone: true,
    imports: [CommonModule, ShrlLayoutComponent, RouterModule],
    providers: [RxState],
})
export class JplMainLayoutComponent {}
