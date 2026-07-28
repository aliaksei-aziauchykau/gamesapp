import { isPlatformServer } from '@angular/common';
import { inject, InjectionToken, PLATFORM_ID } from '@angular/core';

import { WINDOW } from './window.token';

export const CryptToken = new InjectionToken<Crypto>('Crypto', {
    factory() {
        if (isPlatformServer(inject(PLATFORM_ID))) return { randomUUID: () => '' } as unknown as Crypto;

        return inject(WINDOW).crypto;
    },
});
