import { Component, OnDestroy } from '@angular/core';

import { Destroyable } from './destroyable';

@Component({
    template: '',
})
export class DestroyableComponent extends Destroyable implements OnDestroy {
    ngOnDestroy() {
        this._ngOnDestroy();
    }
}
