import { Component, Input } from '@angular/core';

@Component({
    selector: 'shrl-with-button-to-scroll-decorator',
    templateUrl: 'with-button-to-scroll-decorator.component.html',
    styleUrls: ['./with-button-to-scroll-decorator.component.scss'],
})
export class WithButtonToScrollComponent {
    @Input() toLogicAnchorName!: string;
    @Input() tooltipText!: string;
    @Input() mode!: 'dark' | 'light';
    @Input() direction!: 'up' | 'down';
    @Input() buttonName = '';
    @Input() position: 'right' | 'center' = 'right';
}
