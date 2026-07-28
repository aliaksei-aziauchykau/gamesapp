import { InjectionToken, Type } from '@angular/core';

import { IDynamicLoaderConfiguration } from '../interfaces/dynamic-loader-configuration.interface';
import { DynamicBootstrapComponent } from '../models/dynamic-bootstrap-component.model';

export const DynamicLoaderConfigurationToken = new InjectionToken<IDynamicLoaderConfiguration>(
    'Dynamic module configuration',
    { factory: () => ({ devMode: false, moduleConfigs: {} }) },
);

export const DynamicBootstrapComponentToken = new InjectionToken<Type<DynamicBootstrapComponent>>(
    'Dynamic bootstrap component from dynamic module',
);
