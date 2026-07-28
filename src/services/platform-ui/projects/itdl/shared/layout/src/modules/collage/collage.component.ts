import { Component, Input } from '@angular/core';

@Component({
    selector: 'shrl-collage',
    templateUrl: './collage.component.html',
    styleUrls: ['./collage.component.scss'],
})
export class CollageComponent {
    @Input() imageSources!: string[];
}
