import { inject, Injectable } from '@angular/core';
import { Theme } from '@itdl/game-portal/ui';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { CookieService } from 'ngx-cookie-service';

import { ToggleThemeAction, UserPreferencesStoreActions } from './user-preference.actions';

export interface IUserPreferenceState {
    theme: Theme;
}

const defaults: IUserPreferenceState = {
    theme: 'light',
};

@State<IUserPreferenceState>({
    name: 'userPreferences',
    defaults,
})
@Injectable()
export class UserPreferenceState {
    private readonly cookieService = inject(CookieService);

    @Action(ToggleThemeAction)
    public toggleTheme({ patchState, getState }: StateContext<IUserPreferenceState>) {
        const theme = getState().theme;
        const newTheme = theme === 'dark' ? 'light' : 'dark';
        this.cookieService.set('bgp.theme', newTheme);
        patchState({ theme: newTheme });
    }

    @Action(UserPreferencesStoreActions.SetThemeAction)
    public setTheme(
        { patchState, getState }: StateContext<IUserPreferenceState>,
        { theme }: UserPreferencesStoreActions.SetThemeAction,
    ) {
        patchState({ theme });
    }

    @Selector()
    public static theme(state: IUserPreferenceState) {
        return state.theme;
    }
}
