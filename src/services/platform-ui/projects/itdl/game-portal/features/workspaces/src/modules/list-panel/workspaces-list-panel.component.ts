import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule } from '@angular/router';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { GpllPanelComponent } from '@itdl/game-portal/ui/panel';
import { ShrlForeachModule } from '@itdl/shared/common/src/modules/foreach/foreach.module';
import { GpllWorkspacesListFetcherComponent } from '../list-fetcher/workspaces-list-fetcher.component';

@Component({
    selector: 'gpll-workspaces-list-panel',
    templateUrl: './workspaces-list-panel.component.html',
    styleUrl: './workspaces-list-panel.component.scss',
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
        ShrlForeachModule,
        RouterModule,
        GpllPanelComponent,
    ],
})
export class GpllWorkspacesListPanelComponent {}
