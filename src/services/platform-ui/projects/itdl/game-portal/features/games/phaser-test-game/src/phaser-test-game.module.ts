import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ShrlHeaderTemplateModule } from '@itdl/shared/layout';

import { GpllPhaserTestGameComponent } from './phaser-test-game.component';

@NgModule({
    declarations: [GpllPhaserTestGameComponent],
    exports: [GpllPhaserTestGameComponent],
    imports: [CommonModule, ShrlHeaderTemplateModule],
    providers: [],
})
export class GpllPhaserTestGameModule {
    public static readonly entryComponent = GpllPhaserTestGameComponent;
}
