export interface IControlEntitySchema {
    controlType: ControlEntitySchemaType;
    name: string;
    order: number;
}

export type ControlEntitySchemaType = 'input' | 'select' | 'checkbox' | 'rte';
