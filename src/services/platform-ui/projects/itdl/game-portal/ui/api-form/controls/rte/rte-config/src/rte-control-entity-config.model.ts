import { BaseControlEntityConfig, ControlModeType, IControlEntityConfig } from '@itdl/game-portal/ui/api-form/entities';

export class RteControlEntitySchemaConfig {
    placeholder!: string;
}

export class RteControlEntityConfig extends BaseControlEntityConfig {
    override mode: ControlModeType = 'edit';

    constructor(raw: IControlEntityConfig) {
        super();
        Object.assign(this, raw);
    }
}
