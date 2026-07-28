import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GpllLoginContentModule } from '@itdl/game-portal/contents/login-content';
import { GplLoginPageComponent } from './login-page.component';
import { GplLoginPageRoutingModule } from './login-page.routing.module';

@NgModule({
    declarations: [GplLoginPageComponent],
    imports: [CommonModule, GpllLoginContentModule, GplLoginPageRoutingModule],
})
export class GplLoginPageModule {}
