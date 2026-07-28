import { Component, inject, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { RouterOutlet } from '@angular/router';
import { GplAppConfigProviderToken } from '@itdl/game-portal/common/config';
import { UserActions } from '@itdl/game-portal/features/user/ng-xs';
import { isServerPlatformToken } from '@itdl/shared/common';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Store } from '@ngxs/store';
import { L10nIntlModule } from 'angular-l10n';
import { NgcCookieConsentService } from 'ngx-cookieconsent';

@Component({
    selector: 'gpl-app-root',
    standalone: true,
    imports: [RouterOutlet, L10nIntlModule],
    templateUrl: './game-portal.app.component.html',
    styleUrl: './game-portal.app.component.scss',
})
export class GamePortalAppComponent implements OnInit {
    private readonly matIconRegistry = inject(MatIconRegistry);
    private readonly domSanitizer = inject(DomSanitizer);
    private readonly store = inject(Store);
    private readonly isServer = inject(isServerPlatformToken);
    private readonly gplAppConfigProviderToken = inject(GplAppConfigProviderToken);

    constructor(private ccService: NgcCookieConsentService) {}

    @Dispatch() generateUserId = () => new UserActions.GenerateUserId();

    ngOnInit(): void {
        console.log('App config', this.gplAppConfigProviderToken.config);
        if (this.isServer) {
            console.log(
                'Server store',
                this.store.selectSnapshot((state) => state),
            );
        }

        this.generateUserId();
        this.matIconRegistry.addSvgIcon(
            'simple-color-avatar',
            this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/icons/avatars/simple-color-avatar.svg'),
        );
    }
}
