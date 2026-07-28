import { importProvidersFrom } from '@angular/core';
import { NgxStripeModule } from 'ngx-stripe';
import { IEnvironmentVars } from '../jam-portal.app.config';

export const stripeProviders = (envVars: IEnvironmentVars) => {
    return [importProvidersFrom(NgxStripeModule.forRoot(envVars.stripePublicKey))];
};
