import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { GplKrokoGamePageComponent } from './kroko-game-page.component';

const routes: Routes = [{ path: '', component: GplKrokoGamePageComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplKrokoGamePageRoutingModule {}
