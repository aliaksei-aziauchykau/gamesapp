import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JplHomePageComponent } from './home-page.component';

const routes: Routes = [{ path: '', component: JplHomePageComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class JplHomePageRoutingModule {}
