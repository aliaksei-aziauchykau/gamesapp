import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ShrlUiActionsModule } from '@itdl/shared/common/ui-actions';
import { L10nIntlModule } from 'angular-l10n';

@Component({
    selector: 'jpl-app-root',
    standalone: true,
    imports: [RouterOutlet, L10nIntlModule, ShrlUiActionsModule],
    templateUrl: './jam-portal.app.component.html',
    styleUrl: './jam-portal.app.component.scss',
})
export class JamPortalAppComponent {}
