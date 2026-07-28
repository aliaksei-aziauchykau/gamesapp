import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IsGameCodeExistCanActivate } from '../game-page/guards/game-page.guard';
import { GplLobbyPageComponent } from './lobby-page.component';

const routes: Routes = [
    { path: ':gameCode', component: GplLobbyPageComponent, canActivate: [IsGameCodeExistCanActivate] },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GplLobbyPageRoutingModule {}
