import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GpllWorkspacesListPanelComponent } from '../public-api';

@Component({
    selector: 'gpll-workspaces',
    templateUrl: './workspaces.component.html',
    styleUrl: './workspaces.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [CommonModule, GpllWorkspacesListPanelComponent],
})
export class GpllWorkspacesComponent {
    router = inject(Router);
    act = inject(ActivatedRoute);

    constructor() {
        console.log('router', this.router, this.act);
    }
}
