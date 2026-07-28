import { Directive, Input } from '@angular/core';

import { IFormDataSyncDirectiveParams } from '../../models/interfaces/form-data-sync-directive-params.interface';
import { ShrlDefaultFormDataSyncService } from '../../services/default-form-data-sync.service';
import { ShrlFormDataSyncNotificationService } from '../../services/form-data-sync-notification.service';
import { BaseFormDataSyncDirective } from './base-form-data-sync.abstract.directive';

@Directive({
    selector: '[shrlFormDataSync]',
})
export class ShrlFormDataSyncDirective extends BaseFormDataSyncDirective {
    @Input() shrlFormDataSync!: IFormDataSyncDirectiveParams;

    protected override get shrlFormDataSyncDirectiveParams(): IFormDataSyncDirectiveParams {
        return this.shrlFormDataSync;
    }

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor(
        formDataSyncService: ShrlDefaultFormDataSyncService,
        formDataSyncNotificationService: ShrlFormDataSyncNotificationService,
    ) {
        super(formDataSyncService, formDataSyncNotificationService);
    }
}
