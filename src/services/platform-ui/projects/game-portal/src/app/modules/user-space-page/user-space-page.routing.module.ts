import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GplUserSpacePageComponent } from './user-space-page.component';

const routes: Routes = [
    {
        path: '',
        component: GplUserSpacePageComponent,
        children: [
            {
                path: '',
                loadChildren: () =>
                    import('@itdl/game-portal/contents/user-space-content').then((m) => m.GpllUserSpaceContentModule),
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplUserSpacePageRoutingModule {}
