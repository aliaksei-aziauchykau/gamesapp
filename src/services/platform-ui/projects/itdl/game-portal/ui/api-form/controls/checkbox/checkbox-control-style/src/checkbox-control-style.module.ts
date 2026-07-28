import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { GpllCheckboxControlStyleComponent } from './checkbox-control-style.component';

@NgModule({
    imports: [CommonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
    exports: [GpllCheckboxControlStyleComponent],
    declarations: [GpllCheckboxControlStyleComponent],
    providers: [],
})
export class GpllCheckboxControlStyleModule {}
