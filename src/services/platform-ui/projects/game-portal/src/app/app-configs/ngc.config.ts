import { importProvidersFrom } from '@angular/core';
import { NgcCookieConsentConfig, NgcCookieConsentModule } from 'ngx-cookieconsent';

const cookieConfig: NgcCookieConsentConfig = {
    cookie: {
        domain: 'localhost', // or 'your.domain.com' // it is mandatory to set a domain, for cookies to work properly (see https://goo.gl/S2Hy2A)
    },
    palette: {
        popup: {
            background: '#000',
        },
        button: {
            background: '#f1d600',
        },
    },
    theme: 'edgeless',
    type: 'opt-out',
};

export const ngcProviders = () => {
    return [importProvidersFrom(NgcCookieConsentModule.forRoot(cookieConfig))];
};
