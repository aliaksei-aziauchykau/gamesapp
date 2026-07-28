import { IControlEntityConfig } from '../interfaces/control-entity-config.interface';
import { ControlModeType } from '../types/control-mode.type';

export abstract class BaseControlEntityConfig implements IControlEntityConfig {
    mode!: ControlModeType;
    isDebug!: boolean;

    public isMode(mode: ControlModeType): boolean {
        return this.mode === mode;
    }

    public get isEditMode(): boolean {
        return this.isMode('edit');
    }

    public get isViewMode(): boolean {
        return this.isMode('view');
    }
}
