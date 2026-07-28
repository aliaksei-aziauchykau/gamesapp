import { InjectionToken, Type } from '@angular/core';

export const ShrlServerLoaderComponentTypeToken = new InjectionToken<Type<unknown>>('ServerLoaderComponentTypeToken');

export const ShrlServerLoaderComponentInputsToken = new InjectionToken<Record<string, unknown>>(
    'ServerLoaderComponentInputsToken',
);
