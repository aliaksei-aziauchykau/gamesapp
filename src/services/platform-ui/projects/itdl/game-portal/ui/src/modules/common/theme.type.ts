import { InjectionToken } from '@angular/core';

export type Theme = 'dark' | 'light';

export const ThemeToken = new InjectionToken<Theme>('Theme token');
