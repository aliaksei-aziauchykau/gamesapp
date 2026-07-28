import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
    selector: 'gpl-admin-error-page',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './admin-error-page.component.html',
    styleUrls: ['./admin-error-page.component.scss'],
})
export class GplAdminErrorPageComponent {}
