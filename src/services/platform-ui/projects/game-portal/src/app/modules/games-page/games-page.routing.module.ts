import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { gamesByGameDefResolver } from '@itdl/game-portal/features/game/resolvers';

import { GplGamesPageComponent } from './games-page.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'games',
        pathMatch: 'full',
    },
    { path: 'games', component: GplGamesPageComponent, resolve: { data: gamesByGameDefResolver } },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplGamesPageRoutingModule {}
