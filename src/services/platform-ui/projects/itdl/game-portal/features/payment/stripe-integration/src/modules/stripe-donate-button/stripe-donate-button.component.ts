import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { GpllPaymentSessionApiService } from '@itdl/game-portal/features/payment/api';
import { RxState } from '@rx-angular/state';
import { rxActions } from '@rx-angular/state/actions';
import { StripeService } from 'ngx-stripe';
import { switchMap, tap } from 'rxjs';

@Component({
    selector: 'gpll-stripe-donate-button',
    templateUrl: './stripe-donate-button.component.html',
    styleUrls: ['./stripe-donate-button.component.scss'],
    standalone: true,
    imports: [CommonModule, MatButtonModule],
    providers: [RxState],
})
export class GpllStripeDonateButtonComponent {
    public readonly rxActions = rxActions<{ donate: void }>();

    private readonly stripeService = inject(StripeService);

    private readonly paymentSessionService = inject(GpllPaymentSessionApiService);

    private readonly donateEffects$ = this.rxActions.donate$.pipe(
        switchMap(() => this.paymentSessionService.createCheckoutSession()),
        switchMap((session) => this.stripeService.redirectToCheckout({ sessionId: session.value })),
        tap((result) => console.log(result)),
    );

    constructor(private readonly state: RxState<Record<string, never>>) {
        this.state.hold(this.donateEffects$);
    }
}
