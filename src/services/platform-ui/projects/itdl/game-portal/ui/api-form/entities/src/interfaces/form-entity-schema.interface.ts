import { Dictionary } from '@itdl/shared/common';

import { CollectionNameType } from '../types/collection-name.type';
import { IControlEntitySchema } from './control-entity-schema.interface';

export interface IFormEntitySchema {
    collectionName: CollectionNameType;
    controls: Dictionary<string, IControlEntitySchema>;
}
