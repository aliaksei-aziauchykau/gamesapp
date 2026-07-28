import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { GpllRteControlStyleModule } from '@itdl/game-portal/ui/api-form/controls/rte/rte-control-style';
import { SafeHtmlPipe } from '@itdl/shared/pipes/safe-html';
import { EditorModule } from '@tinymce/tinymce-angular';

import { GpllIRteControlComponent } from './rte-control.component';

@NgModule({
    imports: [
        CommonModule,
        EditorModule,
        FormsModule,
        GpllRteControlStyleModule,
        MatCheckboxModule,
        ReactiveFormsModule,
        SafeHtmlPipe,
    ],
    exports: [GpllIRteControlComponent],
    declarations: [GpllIRteControlComponent],
    providers: [],
})
export class GpllRteControlModule {
    public static readonly entryComponent = GpllIRteControlComponent;
}
