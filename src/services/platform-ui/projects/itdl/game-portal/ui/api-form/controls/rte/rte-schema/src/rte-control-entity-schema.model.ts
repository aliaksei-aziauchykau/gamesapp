import { BaseControlEntitySchema, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

export class RteControlEntitySchemaConfig {
    placeholder!: string;
}

export class RteControlEntitySchema extends BaseControlEntitySchema {
    controlType = 'rte' as const;
    value: string | undefined = undefined;
    label!: string;
    placeholder = 'test';
    apiKey!: string;

    isReadonly = false;
    isViewMode = false;
    isDebugMode = true;

    constructor(raw: IControlEntitySchema) {
        super();
        Object.assign(this, raw);
    }
}
