import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllRunningGameContainerComponent } from '@itdl/game-portal/features/game-container';
import { GpllGameContainerSocketComponent } from '@itdl/game-portal/features/game-container/socket';
import { ShrlDynamicLoaderModule } from '@itdl/shared/dynamic-loader';
import { RxLet } from '@rx-angular/template/let';

import { GpllGameContentComponent } from './game-content.component';
import { gameContentLazyModulesConfig } from './game-content.lazy-modules';

@NgModule({
    imports: [
        CommonModule,
        GpllGameContainerSocketComponent,
        GpllRunningGameContainerComponent,
        RxLet,
        ShrlDynamicLoaderModule.configure(gameContentLazyModulesConfig),
    ],
    exports: [GpllGameContentComponent],
    declarations: [GpllGameContentComponent],
    providers: [],
})
export class GpllGameContentModule {}
