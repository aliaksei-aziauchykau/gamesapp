import { ModuleWithProviders, NgModule } from '@angular/core';

import { ShrlSignalrService } from './signalr.service';
import { ISignalrConfig, ShrlSignalrConfigToken } from './signalr.token';

@NgModule({
    providers: [ShrlSignalrService],
})
export class ShrlSignalrModule {
    public static configure(config: ISignalrConfig): ModuleWithProviders<ShrlSignalrModule> {
        return {
            ngModule: ShrlSignalrModule,
            providers: [
                {
                    provide: ShrlSignalrConfigToken,
                    useValue: config,
                },
            ],
        };
    }
}
