import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'gpll-goals-section',
    standalone: true,
    imports: [],
    templateUrl: './goals-section.component.html',
    styleUrl: './goals-section.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllGoalsSectionComponent {}

