import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { GplLoginPageComponent } from './login-page.component';

const routes: Routes = [{ path: '', component: GplLoginPageComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplLoginPageRoutingModule {}
