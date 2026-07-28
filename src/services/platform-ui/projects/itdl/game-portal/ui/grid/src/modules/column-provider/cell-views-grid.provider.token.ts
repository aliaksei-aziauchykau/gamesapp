import { inject, InjectionToken } from '@angular/core';
import { ICellViewsGridProvider } from './cell-views-grid-provider.interface';
import { DefaultCellViewsGridProvider } from './default-cell-views-grid-provider.interface';

export const GpllCellViewsGridProviderToken = new InjectionToken<ICellViewsGridProvider>(
    'Gpll api form cancel action service tokens',
    { providedIn: 'root', factory: () => inject(DefaultCellViewsGridProvider) },
);
