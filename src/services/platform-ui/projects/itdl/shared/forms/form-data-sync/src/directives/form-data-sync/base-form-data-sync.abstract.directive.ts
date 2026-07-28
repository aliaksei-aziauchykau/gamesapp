import { AfterContentInit, Directive } from '@angular/core';
import { DestroyableDirective } from '@itdl/shared/common';
import { merge, Observable, switchMap, takeUntil } from 'rxjs';

import { IFormDataSyncService } from '../../models/form-data-sync-service.abstract.model';
import { IFormDataSyncDirectiveParams } from '../../models/interfaces/form-data-sync-directive-params.interface';
import { ShrlFormDataSyncNotificationService } from '../../services/form-data-sync-notification.service';
import { getAllFormControls } from '../../utils/form.helpers';

@Directive()
export abstract class BaseFormDataSyncDirective extends DestroyableDirective implements AfterContentInit {
    constructor(
        protected readonly formDataSyncService: IFormDataSyncService,
        protected readonly formDataSyncNotificationService: ShrlFormDataSyncNotificationService,
    ) {
        super();
    }

    protected abstract get shrlFormDataSyncDirectiveParams(): IFormDataSyncDirectiveParams;

    public ngAfterContentInit(): void {
        this.formDataSyncNotificationService.isFormSyncRequired$
            .pipe(
                takeUntil(this.onDestroy),
                switchMap(() => this.getControlsObservable()),
            )
            .subscribe();
    }

    private getControlsObservable(): Observable<unknown> {
        const { form, model, service } = this.shrlFormDataSyncDirectiveParams;

        const handler = service || this.formDataSyncService;

        const controls = getAllFormControls(form)
            .filter((control) => handler.filter(control))
            .map((control) => handler.map(control, model, form));

        return merge(...controls).pipe(takeUntil(this.onDestroy));
    }
}
