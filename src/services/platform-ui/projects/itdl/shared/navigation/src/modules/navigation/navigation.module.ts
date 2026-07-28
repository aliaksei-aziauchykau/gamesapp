import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ShrlLogicAnchorDirective } from './directives/logic-anchor.directive';
import { ShrlScrollToSectionDirective } from './directives/scroll-to-section.directive';
import { LogicAnchorStoreService } from './services/logic-anchor-registration.service';
import { NavigationService } from './services/navigation.service';

@NgModule({
    imports: [CommonModule],
    exports: [ShrlLogicAnchorDirective, ShrlScrollToSectionDirective],
    declarations: [ShrlLogicAnchorDirective, ShrlScrollToSectionDirective],
    providers: [LogicAnchorStoreService, NavigationService],
})
export class ShrlNavigationModule {}
