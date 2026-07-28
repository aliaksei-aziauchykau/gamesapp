import { IDynamicModuleConfiguration } from './dynamic-module-configuration.interface';

export interface IDynamicLoaderConfiguration<T extends string | number | symbol = string> {
    devMode: boolean;
    moduleConfigs: Record<T, IDynamicModuleConfiguration>;
}
