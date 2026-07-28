import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSlideToggleChange, MatSlideToggleModule } from '@angular/material/slide-toggle';

import { Theme } from '../common/theme.type';

@Component({
    selector: 'gpll-theme-toggler',
    templateUrl: 'theme-toggler.component.html',
    styleUrls: ['./theme-toggler.component.scss'],
    standalone: true,
    imports: [MatSlideToggleModule],
})
export class GpllThemeTogglerComponent {
    @Input() theme: Theme = 'light';
    @Output() changeEvent = new EventEmitter<MatSlideToggleChange>();

    public change(event: MatSlideToggleChange) {
        this.changeEvent.emit(event);
    }
}
