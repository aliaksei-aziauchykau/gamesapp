import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    templateUrl: './game-page.component.html',
    styleUrls: ['./game-page.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GplGamePageComponent {}
