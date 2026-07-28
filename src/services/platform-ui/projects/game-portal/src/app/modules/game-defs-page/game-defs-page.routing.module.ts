import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
    gameDefinitionByNameResolver,
    gameDefinitionsResolver,
} from '@itdl/game-portal/features/game-definition/resolvers';

import { GplGameDefsPageComponent } from './game-defs-page.component';

const routes: Routes = [
    {
        path: '',
        component: GplGameDefsPageComponent,
        children: [],
        resolve: { data: gameDefinitionsResolver },
    },
    {
        path: ':gameDefinitionName',
        loadChildren: () =>
            import(
                /* webpackChunkName: "gpl-games-page.module.chunk" */
                '../games-page/games-page.module'
            ).then((m) => m.GplGamesPageModule),
        resolve: { data: gameDefinitionByNameResolver },
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplGameDefsPageRoutingModule {}
