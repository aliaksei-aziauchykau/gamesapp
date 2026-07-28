import { CommonModule } from '@angular/common';
import { Component, ContentChild, Input, TemplateRef } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { ShrlForeachModule } from '@itdl/shared/common';

@Component({
    selector: 'gpll-panel',
    templateUrl: 'panel.component.html',
    styleUrl: 'panel.component.scss',
    standalone: true,
    imports: [
        CommonModule,
        MatButtonModule,
        MatSidenavModule,
        MatListModule,
        GpllTranslationModule,
        MatIconModule,
        ShrlForeachModule,
    ],
})
export class GpllPanelComponent {
    @Input() public level = 0;
    @Input() public navigationButtonName = 'Default';

    @ContentChild('panel') public panel?: TemplateRef<unknown>;
    @ContentChild('panelContent') public panelContent?: TemplateRef<unknown>;
}
