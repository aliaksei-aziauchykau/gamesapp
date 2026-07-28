/* eslint-disable @angular-eslint/component-selector */
import { Component, inject, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroupDirective } from '@angular/forms';
import { GpllFormControl, GpllFormGroup } from '@itdl/game-portal/forms';
import {
    GpllControlConfigsToken,
    IControlEntityConfig,
    IControlEntitySchema,
} from '@itdl/game-portal/ui/api-form/entities';

export type ControlConfiguration<TSchema extends IControlEntitySchema, TConfig extends IControlEntityConfig, TValue> = {
    control: GpllFormControl<TValue>;
    schema: TSchema;
    config: TConfig;
    value?: TValue | undefined;
};

@Component({
    selector: '',
    template: '',
})
export abstract class GpllBaseControlComponent<
    TSchema extends IControlEntitySchema,
    TConfig extends IControlEntityConfig,
    TValue,
> implements OnInit, OnDestroy
{
    // @Input() form!: GpllFormGroup;
    @Input() value!: TValue | undefined;
    @Input() public set schema(value: IControlEntitySchema) {
        this._schema = this.getControlEntitySchema(value);
    }

    public get schema(): TSchema {
        return this._schema;
    }

    @Input() public set config(value: IControlEntityConfig) {
        this._config = this.getConfig(value);
    }

    public get config(): TConfig {
        return this._config;
    }

    public get control(): GpllFormControl<TValue> {
        return this._control;
    }

    private readonly controlConfigs = inject(GpllControlConfigsToken, { optional: true }) || [];
    private _schema!: TSchema;
    private _config!: TConfig;
    private _control!: GpllFormControl<TValue>;
    protected readonly form = inject(FormGroupDirective).form as GpllFormGroup;

    protected abstract getControlEntitySchema(value: IControlEntitySchema): TSchema;
    protected abstract getControlEntityConfig(value: IControlEntityConfig): TConfig;

    ngOnInit(): void {
        this._config = this.getConfig(this._config);
        this._control = this.createControl(this._schema);
        this._configureControl({
            control: this._control,
            schema: this._schema,
            config: this._config,
            value: this.value,
        });
        this.form.addControl(this._schema.name, this._control);
    }

    ngOnDestroy(): void {
        this.form.removeControl(this._schema.name);
    }

    protected createControl(schema: TSchema) {
        const control = GpllFormControl.create<TValue>();
        return control;
    }

    private _configureControl(controlConfiguration: ControlConfiguration<TSchema, TConfig, TValue>): void {
        const { control, schema, config, value } = controlConfiguration;

        if (value) control.setValue(value, { emitEvent: false });

        this.configureControl(controlConfiguration);
    }

    private getConfig(input: IControlEntityConfig): TConfig {
        const config = Object.assign({}, ...this.controlConfigs, input);
        return this.getControlEntityConfig(config);
    }

    // eslint-disable-next-line @typescript-eslint/no-empty-function
    protected configureControl(controlConfiguration: ControlConfiguration<TSchema, TConfig, TValue>): void {}
}
