import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { UserPreferenceState } from '@itdl/game-portal/features/user-preferences';
import { NgxsModule } from '@ngxs/store';

import { MainLayoutComponent } from '../main-layout/main-layout.component';
import { GplPagesRoutingModule } from './pages-routing.module';

@NgModule({
    imports: [CommonModule, GplPagesRoutingModule, MainLayoutComponent, NgxsModule.forFeature([UserPreferenceState])],
})
export class GplPagesModule {}
