import { BaseControlEntityConfig, IControlEntityConfig } from '@itdl/game-portal/ui/api-form/entities';

export class InputControlEntityConfig extends BaseControlEntityConfig {
    constructor(raw: IControlEntityConfig) {
        super();
        Object.assign(this, raw);
    }
}
