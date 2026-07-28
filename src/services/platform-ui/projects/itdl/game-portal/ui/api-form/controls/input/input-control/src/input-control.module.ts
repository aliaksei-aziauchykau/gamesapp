import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GpllInputControlStyleModule } from '@itdl/game-portal/ui/api-form/controls/input/input-control-style';

import { GpllInputControlComponent } from './input-control.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        GpllInputControlStyleModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatInputModule,
        ReactiveFormsModule,
    ],
    exports: [GpllInputControlComponent],
    declarations: [GpllInputControlComponent],
    providers: [],
})
export class GpllInputControlModule {
    public static readonly entryComponent = GpllInputControlComponent;
}
