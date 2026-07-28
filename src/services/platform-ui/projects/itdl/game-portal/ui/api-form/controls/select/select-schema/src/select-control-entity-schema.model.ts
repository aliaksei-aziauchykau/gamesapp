import { BaseControlEntitySchema, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

export class SelectControlEntitySchemaConfig {
    placeholder!: string;
}

export class SelectControlEntitySchema extends BaseControlEntitySchema {
    controlType = 'select' as const;
    config = new SelectControlEntitySchemaConfig();
    value: string | undefined = undefined;

    constructor(raw: IControlEntitySchema) {
        super();
        Object.assign(this, raw);
    }
}
