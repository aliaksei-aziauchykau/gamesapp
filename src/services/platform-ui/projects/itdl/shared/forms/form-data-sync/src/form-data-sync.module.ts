import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ShrlFormDataSyncDirective } from './directives/form-data-sync/form-data-sync.directive';
import { ShrlDefaultFormDataSyncService } from './services/default-form-data-sync.service';
import { ShrlFormDataSyncNotificationService } from './services/form-data-sync-notification.service';

@NgModule({
    imports: [CommonModule],
    providers: [ShrlDefaultFormDataSyncService, ShrlFormDataSyncNotificationService],
    declarations: [ShrlFormDataSyncDirective],
    exports: [ShrlFormDataSyncDirective],
})
export class ShrlFormDataSyncModule {}
