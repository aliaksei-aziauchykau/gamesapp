import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
    selector: 'gpll-features-section',
    standalone: true,
    imports: [CommonModule, MatCardModule, MatGridListModule, MatButtonModule],
    templateUrl: './features-section.component.html',
    styleUrls: ['./features-section.component.scss'],
})
export class GpllFeaturesSectionComponent {
    public readonly features = [
        {
            title: 'Vast Selection',
            description: 'Explore an expansive library of games across all genres, curated for gamers of all tastes.',
            icon: 'assets/icons/avatars/simple-color-avatar.svg',
            actionText: 'Learn More',
        },
        {
            title: 'Multiplayer Fun',
            description: 'Connect and compete with thousands of players in our thrilling multiplayer experiences.',
            icon: 'assets/icons/avatars/simple-color-avatar.svg',
            actionText: 'Learn More',
        },
        {
            title: 'Exclusive Content',
            description: 'Gain access to exclusive in-game content, early releases, and members-only events.',
            icon: 'assets/icons/avatars/simple-color-avatar.svg',
            actionText: 'Learn More',
        },
        // ... other features ...
    ];
}
