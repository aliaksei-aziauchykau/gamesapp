import { CommonModule } from '@angular/common';
import { Component, ElementRef } from '@angular/core';
import { Line, LineConfig } from 'konva/lib/shapes/Line';

import { ContainerContextService } from '../container-context.service';
import { ShrlKonvaShapeComponent } from '../shape/shape.component';

@Component({
    selector: 'shrl-konva-line',
    templateUrl: './line.component.html',
    styleUrls: ['./line.component.scss'],
    imports: [CommonModule],
    standalone: true,
})
export class ShrlKonvaLineComponent extends ShrlKonvaShapeComponent<Line, LineConfig> {
    protected override createShape = (config: LineConfig) => new Line({ ...config });

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor(element: ElementRef, parentContainerContext: ContainerContextService) {
        super(element.nativeElement, parentContainerContext);
    }
}
