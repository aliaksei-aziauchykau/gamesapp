import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { GpllAuthButtonComponent } from '@itdl/game-portal/features/auth';
import { ShrlHeaderTemplateModule } from '@itdl/shared/layout';
import { GpllLoginContentComponent } from './login-content.component';

@NgModule({
    imports: [CommonModule, RouterModule, GpllAuthButtonComponent, ShrlHeaderTemplateModule],
    exports: [GpllLoginContentComponent],
    declarations: [GpllLoginContentComponent],
    providers: [],
})
export class GpllLoginContentModule {}
