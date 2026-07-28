import { CdkDrag, CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

interface Sticker {
    content: string;
}

@Component({
    selector: 'gpll-sticky-board-section',
    standalone: true,
    imports: [CommonModule, MatFormFieldModule, MatCardModule, FormsModule, MatInputModule, CdkDrag],
    templateUrl: './sticky-board-section.component.html',
    styleUrls: ['./sticky-board-section.component.scss'],
})
export class GpllStickyBoardSectionComponent {
    stickers: Sticker[] = [];
    stickerContent = '';

    public drop(eventInput: unknown): void {
        const event = eventInput as CdkDragDrop<Sticker[]>;
        moveItemInArray(this.stickers, event.previousIndex, event.currentIndex);
    }

    public addSticker(): void {
        if (this.stickerContent.trim()) {
            this.stickers.push({ content: this.stickerContent.trim() });
            this.stickerContent = '';
        }
    }

    public deleteSticker(sticker: Sticker): void {
        const index = this.stickers.indexOf(sticker);
        if (index > -1) {
            this.stickers.splice(index, 1);
        }
    }
}
