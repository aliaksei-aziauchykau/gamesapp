import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ShrlForeachItemDirective } from './directives/foreach-item.directive';
import { ShrlForeachComponent } from './foreach.component';

@NgModule({
    imports: [CommonModule],
    declarations: [ShrlForeachComponent, ShrlForeachItemDirective],
    exports: [ShrlForeachComponent, ShrlForeachItemDirective],
})
export class ShrlForeachModule {}
