import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { GplGamePageComponent } from './game-page.component';
import { IsGameCodeExistCanActivate } from './guards/game-page.guard';

const routes: Routes = [
    // {
    //     path: '',
    //     redirectTo: 'game',
    //     pathMatch: 'full',
    // },
    { path: ':gameCode', component: GplGamePageComponent, canActivate: [IsGameCodeExistCanActivate] },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplGamePageRoutingModule {}
