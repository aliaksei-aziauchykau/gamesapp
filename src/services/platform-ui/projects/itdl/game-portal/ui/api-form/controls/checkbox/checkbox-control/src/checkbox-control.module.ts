import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { GpllCheckboxControlStyleModule } from '@itdl/game-portal/ui/api-form/controls/checkbox/checkbox-control-style';

import { GpllCheckboxControlComponent } from './checkbox-control.component';

@NgModule({
    imports: [CommonModule, GpllCheckboxControlStyleModule, MatCheckboxModule, ReactiveFormsModule],
    exports: [GpllCheckboxControlComponent],
    declarations: [GpllCheckboxControlComponent],
    providers: [],
})
export class GpllCheckboxControlModule {
    public static readonly entryComponent = GpllCheckboxControlComponent;
}
