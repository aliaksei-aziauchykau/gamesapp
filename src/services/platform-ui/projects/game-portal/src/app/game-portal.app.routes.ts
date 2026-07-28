import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () =>
            import(
                /* webpackChunkName: "gpl-pages.module.chunk" */
                './modules/pages/pages.module'
            ).then((m) => m.GplPagesModule),
    },
];
