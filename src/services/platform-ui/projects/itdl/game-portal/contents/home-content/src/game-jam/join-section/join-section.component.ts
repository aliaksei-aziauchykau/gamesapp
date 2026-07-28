import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

@Component({
    selector: 'gpll-join-section',
    standalone: true,
    imports: [MatCardModule, MatIconModule, MatButtonModule],
    templateUrl: './join-section.component.html',
    styleUrl: './join-section.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllJoinSectionComponent {}

