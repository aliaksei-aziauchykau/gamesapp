import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./features/home/home-page.component').then((m) => m.HomePageComponent),
      },
      {
        path: 'gamedefs',
        loadComponent: () =>
          import('./features/game-defs/game-defs-page.component').then((m) => m.GameDefsPageComponent),
      },
      {
        path: 'game',
        loadComponent: () =>
          import('./features/game/game-page.component').then((m) => m.GamePageComponent),
      },
      {
        path: 'games',
        loadComponent: () =>
          import('./features/games/games-page.component').then((m) => m.GamesPageComponent),
      },
      {
        path: 'lobby',
        loadComponent: () =>
          import('./features/lobby/lobby-page.component').then((m) => m.LobbyPageComponent),
      },
      {
        path: 'space',
        loadComponent: () =>
          import('./features/user-space/user-space-page.component').then((m) => m.UserSpacePageComponent),
      },
      {
        path: 'admin',
        loadComponent: () =>
          import('./features/admin/admin-page.component').then((m) => m.AdminPageComponent),
      },
      {
        path: 'login',
        loadComponent: () =>
          import('./features/login/login-page.component').then((m) => m.LoginPageComponent),
      },
    ],
  },
  { path: '**', redirectTo: '' },
];
