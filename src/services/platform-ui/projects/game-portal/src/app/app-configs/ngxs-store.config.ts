import { importProvidersFrom } from '@angular/core';
import { StorageKeys } from '@itdl/game-portal/common/config';
import { ActionQueueXSState } from '@itdl/game-portal/features/action-queue';
import { AuthXSState } from '@itdl/game-portal/features/auth/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { NgxsDispatchPluginModule } from '@ngxs-labs/dispatch-decorator';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { withExperimentalNgxsPendingTasks } from '@ngxs/store/experimental';
import { environment } from '../../environments/environment';

export const ngXsStoreProviders = () => {
    return [
        importProvidersFrom(
            NgxsModule.forRoot([UserXSState, AuthXSState, ActionQueueXSState], {
                developmentMode: !environment.production,
            }),
        ),
        importProvidersFrom(NgxsStoragePluginModule.forRoot({ keys: StorageKeys.keys })),
        importProvidersFrom(NgxsReduxDevtoolsPluginModule.forRoot()),
        importProvidersFrom(NgxsDispatchPluginModule.forRoot()),
        withExperimentalNgxsPendingTasks(),
    ];
};
