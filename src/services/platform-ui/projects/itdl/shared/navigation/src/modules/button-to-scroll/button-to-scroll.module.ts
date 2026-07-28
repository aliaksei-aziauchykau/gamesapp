import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

import { ShrlScrollToLogicAnchorModule } from '../scroll-to-logic-anchor/scroll-to-logic-anchor.module';
import { ButtonToScrollComponent } from './button-to-scroll.component';

@NgModule({
    imports: [CommonModule, MatButtonModule, MatIconModule, MatTooltipModule, ShrlScrollToLogicAnchorModule],
    exports: [ButtonToScrollComponent],
    declarations: [ButtonToScrollComponent],
    providers: [],
})
export class ButtonToScrollModule {}
