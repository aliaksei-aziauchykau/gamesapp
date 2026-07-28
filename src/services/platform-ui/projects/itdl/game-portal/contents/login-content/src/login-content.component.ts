import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'gpll-login-content',
    templateUrl: './login-content.component.html',
    styleUrls: ['./login-content.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllLoginContentComponent {}
