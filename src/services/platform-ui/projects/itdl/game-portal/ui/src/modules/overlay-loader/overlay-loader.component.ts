import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'gpll-overlay-loader',
    templateUrl: './overlay-loader.component.html',
    styleUrls: ['./overlay-loader.component.scss'],
    standalone: true,
    imports: [MatProgressSpinnerModule],
})
export class GpllOverlaySpinnerComponent implements OnInit, OnDestroy, AfterViewInit {
    @Input() targetElementRef!: ElementRef;

    private overlayElement!: HTMLElement;

    constructor(private renderer: Renderer2, private hostElement: ElementRef) {}

    private get targetElement() {
        return this.targetElementRef?.nativeElement;
    }

    ngOnInit(): void {
        if (!this.targetElement) {
            console.error('OverlaySpinnerComponent: targetElement is required.');
            return;
        }

        const position = getComputedStyle(this.targetElement).position;
        if (position === 'static' || !position) {
            this.renderer.setStyle(this.targetElement, 'position', 'relative');
        }
    }

    ngAfterViewInit(): void {
        this.overlayElement = this.hostElement.nativeElement.firstChild;
        this.renderer.appendChild(this.targetElement, this.overlayElement);
    }

    ngOnDestroy(): void {
        if (this.overlayElement && this.targetElement) {
            this.renderer.removeChild(this.targetElement, this.overlayElement);
        }
    }
}
