import { Component } from '@angular/core';
import { environment } from 'projects/game-portal/src/environments/environment';

@Component({
    selector: 'jpl-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss'],
})
export class JplHomePageComponent {
    environment = environment;
}
