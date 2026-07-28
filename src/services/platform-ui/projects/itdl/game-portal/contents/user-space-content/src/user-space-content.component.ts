import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RxState } from '@rx-angular/state';

@Component({
    selector: 'gpll-user-space-content',
    templateUrl: './user-space-content.component.html',
    styleUrls: ['./user-space-content.component.scss'],
    providers: [RxState],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllUserSpaceContentComponent {}
