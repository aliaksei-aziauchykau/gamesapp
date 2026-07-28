import { BaseControlEntitySchema, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

export class CheckboxControlEntitySchemaConfig {
    placeholder!: string;
}

export class CheckboxControlEntitySchema extends BaseControlEntitySchema {
    controlType = 'checkbox' as const;
    // config = new InputControlSchemaConfig();
    placeholder = 'test';
    value: string | undefined = undefined;
    label!: string;

    constructor(raw: IControlEntitySchema) {
        super();
        Object.assign(this, raw);
    }
}
