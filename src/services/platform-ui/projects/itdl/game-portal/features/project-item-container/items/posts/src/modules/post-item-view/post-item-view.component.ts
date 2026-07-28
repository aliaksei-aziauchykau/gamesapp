import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Post } from '@itdl/game-portal/features/project-item-container/entities';

@Component({
    selector: 'gpll-post-item-view',
    templateUrl: './post-item-view.component.html',
    styleUrls: ['./post-item-view.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [CommonModule, MatCardModule, MatButtonModule],
})
export class GpllPostItemViewComponent {
    @Input() public post!: Post;
}
