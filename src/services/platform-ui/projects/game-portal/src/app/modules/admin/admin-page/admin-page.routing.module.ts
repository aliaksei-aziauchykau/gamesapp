import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAdminPageCanActivate } from '@itdl/game-portal/contents/admin/src/guards/admin-page.guard';

import { GplAdminErrorPageComponent } from './admin-error-page/admin-error-page.component';
import { GplAdminPageComponent } from './admin-page.component';

const routes: Routes = [
    {
        path: '',
        component: GplAdminPageComponent,
        canActivate: [IsAdminPageCanActivate],
        children: [],
    },
    {
        path: 'error',
        component: GplAdminErrorPageComponent,
        children: [],
    },
    {
        path: '**',
        redirectTo: '',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplAdminPageRoutingModule {}
