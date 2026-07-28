import { Dictionary } from '@itdl/shared/common';

import { IControlEntitySchema } from '../interfaces/control-entity-schema.interface';
import { IFormEntitySchema } from '../interfaces/form-entity-schema.interface';
import { CollectionNameType } from '../types/collection-name.type';

export class FormEntitySchema implements IFormEntitySchema {
    collectionName!: CollectionNameType;
    controls: Dictionary<string, IControlEntitySchema> = {};

    constructor(raw: IFormEntitySchema) {
        Object.assign(this, raw);
    }
}
