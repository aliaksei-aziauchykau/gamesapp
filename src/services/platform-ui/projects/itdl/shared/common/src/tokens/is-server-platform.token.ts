import { isPlatformServer } from '@angular/common';
import { inject, InjectionToken, PLATFORM_ID } from '@angular/core';

export const isServerPlatformToken = new InjectionToken<boolean>('Is server?', {
    factory() {
        return isPlatformServer(inject(PLATFORM_ID));
    },
});
