import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'gpl-lobby-page',
    templateUrl: './lobby-page.component.html',
    styleUrls: ['./lobby-page.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GplLobbyPageComponent {}
