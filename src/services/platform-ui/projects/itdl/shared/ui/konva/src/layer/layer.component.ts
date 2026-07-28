import { CommonModule } from '@angular/common';
import { Component, ElementRef, SkipSelf } from '@angular/core';
import { Layer, LayerConfig } from 'konva/lib/Layer';

import { ShrlKonvaContainerComponent } from '../container/container.component';
import { ContainerContextService } from '../container-context.service';

@Component({
    selector: 'shrl-konva-layer',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './layer.component.html',
    styleUrls: ['./layer.component.scss'],
    providers: [ContainerContextService],
})
export class ShrlKonvaLayerComponent extends ShrlKonvaContainerComponent<Layer, LayerConfig> {
    protected override createContainer(config: LayerConfig): Layer {
        return new Layer({ ...config });
    }

    constructor(
        element: ElementRef,
        currentContainerContext: ContainerContextService,
        @SkipSelf() parentContainerContext: ContainerContextService,
    ) {
        super(element.nativeElement, currentContainerContext, parentContainerContext);
    }
}
