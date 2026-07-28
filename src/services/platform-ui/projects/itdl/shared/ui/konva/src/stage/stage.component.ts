import { CommonModule } from '@angular/common';
import { Component, ElementRef } from '@angular/core';
import { Stage, StageConfig } from 'konva/lib/Stage';

import { ShrlKonvaContainerComponent } from '../container/container.component';
import { ContainerContextService } from '../container-context.service';

type StageConfigOmitted = Omit<StageConfig, 'container'>;

@Component({
    selector: 'shrl-konva-stage',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './stage.component.html',
    styleUrls: ['./stage.component.scss'],
    providers: [ContainerContextService],
})
export class ShrlKonvaStageComponent extends ShrlKonvaContainerComponent<Stage, StageConfigOmitted> {
    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor(element: ElementRef, currentContainerContext: ContainerContextService) {
        super(element.nativeElement, currentContainerContext);
    }

    protected override createContainer(config: StageConfigOmitted, element: HTMLDivElement): Stage {
        return new Stage({ ...config, container: element });
    }
}
