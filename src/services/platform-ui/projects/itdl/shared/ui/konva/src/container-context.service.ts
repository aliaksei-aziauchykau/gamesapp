import { ReactiveValue } from '@itdl/shared/common';
import { Group } from 'konva/lib/Group';
import { Layer } from 'konva/lib/Layer';
import { Stage } from 'konva/lib/Stage';

export class ContainerContextService extends ReactiveValue<ContainerType> {
    constructor() {
        super(null);
    }
}

export type ContainerType = Stage | Group | Layer;
