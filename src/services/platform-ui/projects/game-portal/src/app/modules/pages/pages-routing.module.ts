import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainLayoutComponent } from '../main-layout/main-layout.component';

const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            {
                path: '',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "gpl-home-page.module.chunk" */
                        '../home-page/home-page.module'
                    ).then((m) => m.GplHomePageModule),
            },
            // {
            //     path: 'gamedefs/:code',
            //     redirectTo: 'gamedefs',
            // },
            {
                path: 'gamedefs',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "gpl-game-defs-page.module.chunk" */
                        '../game-defs-page/game-defs-page.module'
                    ).then((m) => m.GplGameDefsPageModule),
            },
            {
                path: 'game',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "gpl-game-page.module.chunk" */
                        '../game-page/game-page.module'
                    ).then((m) => m.GplGamePageModule),
            },
            {
                path: 'admin',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "gpl-admin-page.module.chunk" */
                        '../admin/admin-page/admin-page.module'
                    ).then((m) => m.GplAdminPageModule),
            },
            {
                path: 'lobby',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "gpl-lobby-page.module.chunk" */
                        '../lobby-page/lobby-page.module'
                    ).then((m) => m.GplLobbyPageModule),
            },
            {
                path: 'space',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "gpl-user-space-page.module.chunk" */
                        '../user-space-page/user-space-page.module'
                    ).then((m) => m.GplUserSpacePageModule),
            },
            {
                path: 'login',
                loadChildren: () =>
                    import(
                        /* webpackChunkName: "gpl-login-space-page.module.chunk" */
                        '../login-page/login-page.module'
                    ).then((m) => m.GplLoginPageModule),
            },
        ],
    },
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplPagesRoutingModule {}
