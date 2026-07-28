import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { StripeElementsOptions, StripeExpressCheckoutElementOptions } from '@stripe/stripe-js';
import { injectStripe, StripeElementsDirective, StripeExpressCheckoutComponent } from 'ngx-stripe';

@Component({
    selector: 'gpll-stripe-express-checkout',
    templateUrl: './stripe-express-checkout.component.html',
    styleUrls: ['./stripe-express-checkout.component.scss'],
    imports: [CommonModule, StripeElementsDirective, StripeExpressCheckoutComponent],
    standalone: true,
})
export class GpllStripeExpressCheckoutComponent {
    stripe = injectStripe('pk_test_hyAomfLH7SvDOmecyWStO1ak');
    elementsOptions: StripeElementsOptions = {
        mode: 'payment',
        amount: 1099,
        currency: 'usd',
        locale: 'es',
    };

    options: StripeExpressCheckoutElementOptions = {
        buttonType: {
            applePay: 'buy',
            googlePay: 'buy',
        },
    };

    // eslint-disable-next-line @typescript-eslint/no-empty-function
    public pay() {}
}
