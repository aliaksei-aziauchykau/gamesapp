import { Directive, OnDestroy } from '@angular/core';

import { Destroyable } from './destroyable';

@Directive()
export class DestroyableDirective extends Destroyable implements OnDestroy {
    ngOnDestroy() {
        this._ngOnDestroy();
    }
}
