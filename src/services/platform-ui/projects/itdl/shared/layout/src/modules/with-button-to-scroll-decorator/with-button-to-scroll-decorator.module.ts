import { NgModule } from '@angular/core';
import { ButtonToScrollModule } from '@itdl/shared/navigation';

import { CommonModule } from '@angular/common';
import { WithButtonToScrollComponent } from './with-button-to-scroll-decorator.component';

@NgModule({
    imports: [ButtonToScrollModule, CommonModule],
    exports: [WithButtonToScrollComponent],
    declarations: [WithButtonToScrollComponent],
    providers: [],
})
export class ShrlWithButtonToScrollModule {}
