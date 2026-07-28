import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () =>
            import(
                /* webpackChunkName: "jpl-pages.module.chunk" */
                './modules/pages/pages.module'
            ).then((m) => m.JplPagesModule),
    },
];
