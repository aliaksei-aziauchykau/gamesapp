import { Component, HostBinding, Input } from '@angular/core';

type StyleMode = 'dark' | 'light';
type DirectionMode = 'up' | 'down';

@Component({
    selector: 'shrl-button-to-scroll',
    templateUrl: 'button-to-scroll.component.html',
    styleUrls: ['./button-to-scroll.component.scss'],
})
export class ButtonToScrollComponent {
    @Input() logicAnchorName!: string;
    @Input() tooltipText!: string;
    @Input() mode!: StyleMode;
    @Input() direction!: DirectionMode;
    @Input() buttonName = '';

    @HostBinding('class')
    public get hostClasses(): string {
        return [
            this.mode === 'dark' ? 'button-to-scroll--dark' : null,
            this.mode === 'light' ? 'button-to-scroll--light' : null,
        ].join(' ');
    }

    public readonly tooltipClasses: Record<StyleMode, string> = {
        'dark': 'button-to-scroll-tooltip--dark',
        'light': 'button-to-scroll-tooltip--light',
    };

    public readonly icons: Record<DirectionMode, string> = {
        'up': 'arrow_upward',
        'down': 'arrow_downward',
    };
}
