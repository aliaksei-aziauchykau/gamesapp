import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { GpllInputControlStyleComponent } from './input-control-style.component';

@NgModule({
    imports: [CommonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
    exports: [GpllInputControlStyleComponent],
    declarations: [GpllInputControlStyleComponent],
    providers: [],
})
export class GpllInputControlStyleModule {}
