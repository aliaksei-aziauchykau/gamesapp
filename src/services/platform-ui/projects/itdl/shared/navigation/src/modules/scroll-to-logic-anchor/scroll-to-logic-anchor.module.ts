import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ShrlNavigationModule } from '../navigation/navigation.module';
import { ShrlScrollToLogicAnchorDirective } from './scroll-to-logic-anchor.directive';

@NgModule({
    declarations: [ShrlScrollToLogicAnchorDirective],
    exports: [ShrlScrollToLogicAnchorDirective],
    imports: [CommonModule, ShrlNavigationModule],
})
export class ShrlScrollToLogicAnchorModule {}
