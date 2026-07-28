import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GpllUserSpaceContentComponent } from './user-space-content.component';

const routes: Routes = [
    {
        path: '',
        component: GpllUserSpaceContentComponent,
        children: [
            {
                path: 'workspaces',
                loadComponent: () =>
                    import('@itdl/game-portal/features/workspaces').then((m) => m.GpllWorkspacesListPanelComponent),
                children: [
                    {
                        path: ':workspaceId',
                        loadComponent: () =>
                            import('@itdl/game-portal/features/workspaces').then(
                                (m) => m.GpllWorkspacesItemPanelComponent,
                            ),
                        children: [
                            {
                                path: 'projects',
                                loadComponent: () =>
                                    import('@itdl/game-portal/features/projects').then(
                                        (m) => m.GpllProjectsListPanelComponent,
                                    ),
                                children: [
                                    {
                                        path: ':projectId',
                                        loadComponent: () =>
                                            import('@itdl/game-portal/features/projects').then(
                                                (m) => m.GpllProjectsItemPanelComponent,
                                            ),
                                    },
                                ],
                            },
                        ],
                    },
                ],
            },
        ],
    },
];

// const routes: Routes = [
//     {
//         path: '',
//         component: GpllUserSpaceContentComponent,
//         children: [
//             {
//                 path: 'workspaces',
//                 loadChildren: () => import('@itdl/game-portal/features/workspaces').then((m) => m.GpllWorkspacesModule),
//             },
//         ],
//     },
// ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplUserSpaceContentRoutingModule {}
