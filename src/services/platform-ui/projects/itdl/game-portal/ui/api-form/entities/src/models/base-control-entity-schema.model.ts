import { ControlEntitySchemaType, IControlEntitySchema } from '../interfaces/control-entity-schema.interface';

export abstract class BaseControlEntitySchema implements IControlEntitySchema {
    abstract controlType: ControlEntitySchemaType;
    order!: number;
    name!: string;
}
