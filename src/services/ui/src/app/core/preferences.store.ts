import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';

export type PortalTheme = 'dark' | 'light';

type PreferencesState = {
  theme: PortalTheme;
};

const initial: PreferencesState = { theme: 'dark' };

export const PreferencesStore = signalStore(
  { providedIn: 'root' },
  withState(initial),
  withMethods((store) => ({
    setTheme(theme: PortalTheme) {
      patchState(store, { theme });
      document.body.classList.toggle('theme-light', theme === 'light');
      document.body.classList.toggle('theme-dark', theme === 'dark');
    },
    toggleTheme() {
      const next: PortalTheme = store.theme() === 'dark' ? 'light' : 'dark';
      patchState(store, { theme: next });
      document.body.classList.toggle('theme-light', next === 'light');
      document.body.classList.toggle('theme-dark', next === 'dark');
    },
  })),
);
