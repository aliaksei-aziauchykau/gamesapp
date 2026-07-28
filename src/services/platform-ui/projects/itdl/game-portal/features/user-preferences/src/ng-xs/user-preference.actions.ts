import { Theme } from '@itdl/game-portal/ui';

export class ToggleThemeAction {
    static readonly type = '[UserPreference] Toggle Theme';
}

export namespace UserPreferencesStoreActions {
    export class SetThemeAction {
        static readonly type = '[UserPreference] Set Theme';
        constructor(public theme: Theme) {}
    }
}
