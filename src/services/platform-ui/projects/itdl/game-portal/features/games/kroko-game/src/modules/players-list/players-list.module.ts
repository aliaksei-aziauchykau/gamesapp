import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ShrlSignalrModule } from '@itdl/shared/signalr';

import { GpllPlayersListComponent } from './players-list.component';

@NgModule({
    declarations: [GpllPlayersListComponent],
    imports: [CommonModule, MatButtonModule, MatCardModule, MatDividerModule, MatProgressBarModule, ShrlSignalrModule],
    exports: [GpllPlayersListComponent],
})
export class GpllPlayersListModule {}
