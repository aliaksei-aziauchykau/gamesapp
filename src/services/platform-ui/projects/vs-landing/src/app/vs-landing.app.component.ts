import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
    selector: 'vs-app-root',
    standalone: true,
    imports: [RouterOutlet],
    templateUrl: './vs-landing.app.component.html',
    styleUrl: './vs-landing.app.component.scss',
})
export class VsLandingAppComponent {
    title = 'playground';
}
