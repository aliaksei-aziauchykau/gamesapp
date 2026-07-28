import { Component, Input } from '@angular/core';

@Component({
    selector: 'jpll-home-content',
    styleUrls: ['./home-content.component.scss'],
    templateUrl: './home-content.component.html',
    providers: [],
})
export class JpllHomeContentComponent {
    @Input() tinyMceApiKey!: string;
}
