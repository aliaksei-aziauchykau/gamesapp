import { ControlEntitySchemaType } from '@itdl/game-portal/ui/api-form/entities';
import { IDynamicLoaderConfiguration } from '@itdl/shared/dynamic-loader';

export const controlLoaderLazyModulesConfig: IDynamicLoaderConfiguration<ControlEntitySchemaType> = {
    devMode: true,
    moduleConfigs: {
        ['input']: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-input-control.module.chunk" */
                    '@itdl/game-portal/ui/api-form/controls/input/input-control'
                ).then((m) => m.GpllInputControlModule),
        },
        ['select']: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-select-control.module.chunk" */
                    '@itdl/game-portal/ui/api-form/controls/select/select-control'
                ).then((m) => m.GpllSelectControlModule),
        },
        ['checkbox']: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-checkbox-control.module.chunk" */
                    '@itdl/game-portal/ui/api-form/controls/checkbox/checkbox-control'
                ).then((m) => m.GpllCheckboxControlModule),
        },
        ['rte']: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-rte-control.module.chunk" */
                    '@itdl/game-portal/ui/api-form/controls/rte/rte-control'
                ).then((m) => m.GpllRteControlModule),
        },
    },
};
