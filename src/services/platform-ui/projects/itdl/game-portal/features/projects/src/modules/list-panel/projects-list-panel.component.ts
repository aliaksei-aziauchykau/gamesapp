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
import { GpllProjectsListFetcherComponent } from '../list-fetcher/projects-list-fetcher.component';

@Component({
    selector: 'gpll-projects-list-panel',
    templateUrl: './projects-list-panel.component.html',
    styleUrl: './projects-list-panel.component.scss',
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
        ShrlForeachModule,
        RouterModule,
        GpllPanelComponent,
    ],
})
export class GpllProjectsListPanelComponent {}
