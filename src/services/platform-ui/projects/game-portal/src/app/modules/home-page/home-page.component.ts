import { Component } from '@angular/core';
import { environment } from 'projects/game-portal/src/environments/environment';

@Component({
    selector: 'gpl-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss'],
})
export class GplHomePageComponent {
    environment = environment;
}
