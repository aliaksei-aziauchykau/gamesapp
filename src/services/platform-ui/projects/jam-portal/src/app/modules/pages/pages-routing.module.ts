import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { JplMainLayoutComponent } from '../main-layout/main-layout.component';

const routes: Routes = [
    {
        path: '',
        component: JplMainLayoutComponent,
        children: [
            {
                path: '',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "jpl-home-page.module.chunk" */
                        '../home-page/home-page.module'
                    ).then((m) => m.JplHomePageModule),
            },
        ],
    },
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class JplPagesRoutingModule {}
