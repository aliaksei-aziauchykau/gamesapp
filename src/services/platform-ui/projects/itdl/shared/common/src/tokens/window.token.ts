import { inject, InjectionToken } from '@angular/core';
import { ssrWindow } from 'ssr-window';

import { isServerPlatformToken } from './is-server-platform.token';

export const WINDOW = new InjectionToken<Window>('Global window object', {
    factory: () => {
        const isServerPlatform = inject(isServerPlatformToken);
        if (isServerPlatform) return ssrWindow as unknown as Window & typeof globalThis;

        return window;
    },
});
