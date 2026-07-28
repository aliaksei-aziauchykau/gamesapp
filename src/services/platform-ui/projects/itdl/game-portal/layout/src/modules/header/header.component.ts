import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { GpllAuthButtonComponent, GpllAuthContextComponent } from '@itdl/game-portal/features/auth';
import { GpllRolesEnum } from '@itdl/game-portal/features/auth/entities';
import { AuthXSState } from '@itdl/game-portal/features/auth/ng-xs';
import { ToggleThemeAction, UserPreferenceState } from '@itdl/game-portal/features/user-preferences';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { GpllPulseLoaderComponent, GpllThemeTogglerComponent, Theme } from '@itdl/game-portal/ui';
import { isNotNullOrUndefined } from '@itdl/shared/common';
import { IShrlServerLoaderConfig, ShrlServerLoaderComponentTypeToken, ShrlServerLoaderModule } from '@itdl/shared/ui';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { RxActionFactory } from '@rx-angular/state/actions';
import { RxLet } from '@rx-angular/template/let';
import { switchMap } from 'rxjs';

type State = {
    theme: Theme;
    isAdmin: boolean;
    isAuthenticated: boolean;
};

@Component({
    selector: 'gpll-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
    standalone: true,
    imports: [
        CommonModule,
        GpllThemeTogglerComponent,
        MatButtonModule,
        MatIconModule,
        MatSlideToggleModule,
        MatToolbarModule,
        GpllAuthButtonComponent,
        RouterModule,
        RxLet,
        GpllTranslationModule,
        GpllAuthContextComponent,
        ShrlServerLoaderModule,
    ],
    providers: [
        RxState,
        RxActionFactory,
        { provide: ShrlServerLoaderComponentTypeToken, useValue: GpllPulseLoaderComponent },
    ],
})
export class GpllHeaderComponent {
    @Input() sidenav!: MatSidenav;

    readonly theme$ = this.state.select('theme');
    readonly actions = this.actionFactory.create();
    readonly isAdmin$ = this.state.select('isAdmin');
    readonly isAuthenticated$ = this.state.select('isAuthenticated');

    public readonly loaderConfig: IShrlServerLoaderConfig = {
        componentInputs: {
            hostClasses: ['header'],
        },
    };

    constructor(
        private readonly store: Store,
        private readonly state: RxState<State>,
        public readonly actionFactory: RxActionFactory<{ themeToggleChange: void }>,
    ) {
        this.state.connect('theme', this.store.select(UserPreferenceState.theme));
        this.state.hold(
            this.actions.themeToggleChange$.pipe(switchMap(() => this.store.dispatch(new ToggleThemeAction()))),
        );
        this.state.connect('isAdmin', this.store.select(AuthXSState.isHasRole(GpllRolesEnum.Admin)));
        this.state.connect(
            'isAuthenticated',
            this.store.select(AuthXSState.isAuthenticated).pipe(isNotNullOrUndefined()),
        );
    }
}
