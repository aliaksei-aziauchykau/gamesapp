import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
    selector: 'jpll-footer-section',
    standalone: true,
    imports: [MatIconModule],
    templateUrl: './footer-section.component.html',
    styleUrl: './footer-section.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JpllFooterSectionComponent {}

