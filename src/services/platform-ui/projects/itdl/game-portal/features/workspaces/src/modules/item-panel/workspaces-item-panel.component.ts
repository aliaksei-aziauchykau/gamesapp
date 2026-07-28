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
import { GpllWorkspacesItemFetcherComponent } from '../item-fetcher/workspaces-item-fetcher.component';
import { GpllWorkspacesListFetcherComponent } from '../list-fetcher/workspaces-list-fetcher.component';

@Component({
    selector: 'gpll-workspaces-item-panel',
    templateUrl: './workspaces-item-panel.component.html',
    styleUrl: './workspaces-item-panel.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        CommonModule,
        MatButtonModule,
        MatSidenavModule,
        MatListModule,
        GpllTranslationModule,
        MatIconModule,
        GpllWorkspacesListFetcherComponent,
        GpllWorkspacesItemFetcherComponent,
        ShrlForeachModule,
        RouterModule,
        GpllPanelComponent,
        // GpllWorkspacesItemPanelRoutingModule,
    ],
})
export class GpllWorkspacesItemPanelComponent {
    public activateRoute = inject(ActivatedRoute);
    public workspaceId = this.activateRoute.snapshot.params['workspaceId'];
}
