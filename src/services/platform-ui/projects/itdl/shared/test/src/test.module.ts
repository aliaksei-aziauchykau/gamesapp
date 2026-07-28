import { NgModule } from '@angular/core';

import { ShrlTestComponent } from './test.component';

@NgModule({
    imports: [],
    exports: [ShrlTestComponent],
    declarations: [ShrlTestComponent],
    providers: [],
    bootstrap: [],
})
export class ShrlTestModule {
    public readonly entryComponent = ShrlTestComponent;
}
