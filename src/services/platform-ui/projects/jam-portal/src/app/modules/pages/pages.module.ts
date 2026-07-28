import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { JplMainLayoutComponent } from '../main-layout/main-layout.component';
import { JplPagesRoutingModule } from './pages-routing.module';

@NgModule({
    imports: [CommonModule, JplMainLayoutComponent, JplPagesRoutingModule],
})
export class JplPagesModule {}
