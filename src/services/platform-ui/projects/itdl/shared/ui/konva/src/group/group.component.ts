import { CommonModule } from '@angular/common';
import { Component, ElementRef, SkipSelf } from '@angular/core';
import { Group, GroupConfig } from 'konva/lib/Group';

import { ShrlKonvaContainerComponent } from '../container/container.component';
import { ContainerContextService } from '../container-context.service';

@Component({
    selector: 'shrl-konva-group',
    templateUrl: './group.component.html',
    styleUrls: ['./group.component.scss'],
    imports: [CommonModule],
    standalone: true,
    providers: [ContainerContextService],
})
export class ShrlKonvaGroupComponent extends ShrlKonvaContainerComponent<Group, GroupConfig> {
    protected override createContainer(config: GroupConfig): Group {
        return new Group({ ...config });
    }

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor(
        element: ElementRef,
        currentContainerContext: ContainerContextService,
        @SkipSelf() parentContainerContext: ContainerContextService,
    ) {
        super(element.nativeElement, currentContainerContext, parentContainerContext);
    }
}
