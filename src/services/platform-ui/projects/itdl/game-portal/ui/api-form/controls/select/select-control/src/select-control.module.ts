import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GpllApiFormApiService } from '@itdl/game-portal/ui/api-form/api';
import { GpllSelectControlStyleModule } from '@itdl/game-portal/ui/api-form/controls/select/select-control-style';

import { GpllSelectControlComponent } from './select-control.component';

@NgModule({
    imports: [CommonModule, GpllSelectControlStyleModule],
    exports: [GpllSelectControlComponent],
    declarations: [GpllSelectControlComponent],
    providers: [GpllApiFormApiService],
})
export class GpllSelectControlModule {
    public static readonly entryComponent = GpllSelectControlComponent;
}
