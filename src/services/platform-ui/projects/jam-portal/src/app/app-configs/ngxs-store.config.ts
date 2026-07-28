import { importProvidersFrom } from '@angular/core';
import { NgxsDispatchPluginModule } from '@ngxs-labs/dispatch-decorator';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { withExperimentalNgxsPendingTasks } from '@ngxs/store/experimental';
import { environment } from '../../environments/environment';

export const ngXsStoreProviders = () => {
    return [
        importProvidersFrom(
            NgxsModule.forRoot([], {
                developmentMode: !environment.production,
            }),
        ),
        importProvidersFrom(NgxsStoragePluginModule.forRoot({ keys: [] })),
        importProvidersFrom(NgxsReduxDevtoolsPluginModule.forRoot()),
        importProvidersFrom(NgxsDispatchPluginModule.forRoot()),
        withExperimentalNgxsPendingTasks(),
    ];
};
