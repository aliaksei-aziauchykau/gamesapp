import { BaseControlEntitySchema, ControlModeType, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

export class InputControlSchemaConfig {
    placeholder!: string;
    mode: ControlModeType = 'view';
    isReadonly = false;
}

export class InputControlEntitySchema extends BaseControlEntitySchema {
    controlType = 'input' as const;
    config = new InputControlSchemaConfig();
    value: string | undefined = undefined;
    label!: string;
    placeholder!: string;

    constructor(raw: IControlEntitySchema) {
        super();
        Object.assign(this, raw);
    }
}
