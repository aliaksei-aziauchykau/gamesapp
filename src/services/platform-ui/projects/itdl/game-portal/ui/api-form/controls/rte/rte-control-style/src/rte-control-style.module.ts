import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { GpllRteControlStyleComponent } from './rte-control-style.component';

@NgModule({
    imports: [CommonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
    exports: [GpllRteControlStyleComponent],
    declarations: [GpllRteControlStyleComponent],
    providers: [],
})
export class GpllRteControlStyleModule {}
