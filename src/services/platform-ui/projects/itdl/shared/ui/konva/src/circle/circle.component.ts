import { CommonModule } from '@angular/common';
import { Component, ElementRef } from '@angular/core';
import { Circle, CircleConfig } from 'konva/lib/shapes/Circle';

import { ContainerContextService } from '../container-context.service';
import { ShrlKonvaShapeComponent } from '../shape/shape.component';

@Component({
    selector: 'shrl-konva-circle',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './circle.component.html',
    styleUrls: ['./circle.component.scss'],
})
export class ShrlKonvaCircleComponent extends ShrlKonvaShapeComponent<Circle, CircleConfig> {
    protected override createShape(config: CircleConfig) {
        return new Circle(config);
    }

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor(element: ElementRef, parentContainerContext: ContainerContextService) {
        super(element.nativeElement, parentContainerContext);
    }
}
