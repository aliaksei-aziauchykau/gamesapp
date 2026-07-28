import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { GplHomePageComponent } from './home-page.component';

const routes: Routes = [{ path: '', component: GplHomePageComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplHomePageRoutingModule {}
