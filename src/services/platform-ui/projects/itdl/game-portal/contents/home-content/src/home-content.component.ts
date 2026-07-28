import { Component, Input } from '@angular/core';
import { GpllPaymentSessionApiService } from '@itdl/game-portal/features/payment/api';

@Component({
    selector: 'gpll-home-content',
    styleUrls: ['./home-content.component.scss'],
    templateUrl: './home-content.component.html',
    providers: [GpllPaymentSessionApiService],
})
export class GpllHomeContentComponent {
    @Input() tinyMceApiKey!: string;
}
