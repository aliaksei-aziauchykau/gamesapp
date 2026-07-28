import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { GpllPanelComponent } from '@itdl/game-portal/ui/panel';
import { ShrlForeachModule } from '@itdl/shared/common/src/modules/foreach/foreach.module';
import { GpllProjectsItemFetcherComponent } from '../item-fetcher/projects-item-fetcher.component';
import { GpllProjectsListFetcherComponent } from '../list-fetcher/projects-list-fetcher.component';

@Component({
    selector: 'gpll-projects-item-panel',
    templateUrl: './projects-item-panel.component.html',
    styleUrl: './projects-item-panel.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        CommonModule,
        MatButtonModule,
        MatSidenavModule,
        MatListModule,
        GpllTranslationModule,
        MatIconModule,
        GpllProjectsListFetcherComponent,
        GpllProjectsItemFetcherComponent,
        ShrlForeachModule,
        RouterModule,
        GpllPanelComponent,
        // GpllProjectsItemPanelRoutingModule,
    ],
})
export class GpllProjectsItemPanelComponent {
    public activateRoute = inject(ActivatedRoute);
    public projectId = this.activateRoute.snapshot.params['projectId'];

    constructor() {
        console.log(this.activateRoute);
    }
}
