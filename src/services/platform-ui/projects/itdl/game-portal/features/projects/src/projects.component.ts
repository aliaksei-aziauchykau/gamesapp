import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GpllProjectsListPanelComponent } from './modules/list-panel/projects-list-panel.component';

@Component({
    selector: 'gpll-projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [GpllProjectsListPanelComponent],
})
export class GpllProjectsComponent {}
