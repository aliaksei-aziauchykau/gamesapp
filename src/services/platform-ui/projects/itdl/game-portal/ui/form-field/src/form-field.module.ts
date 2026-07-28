import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { GpllFormFieldComponent } from './form-field.component';

@NgModule({
    imports: [CommonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
    exports: [GpllFormFieldComponent],
    declarations: [GpllFormFieldComponent],
    providers: [],
})
export class GpllFormFieldModule {}
