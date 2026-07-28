import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GpllBaseControlComponent } from '@itdl/game-portal/ui/api-form/controls/base';
import { InputControlEntityConfig } from '@itdl/game-portal/ui/api-form/controls/input/input-config';
import { InputControlEntitySchema } from '@itdl/game-portal/ui/api-form/controls/input/input-schema';
import { IControlEntityConfig, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

@Component({
    selector: 'gpll-input-control',
    templateUrl: './input-control.component.html',
    styleUrls: ['./input-control.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllInputControlComponent extends GpllBaseControlComponent<
    InputControlEntitySchema,
    InputControlEntityConfig,
    string
> {
    protected override getControlEntitySchema = (value: IControlEntitySchema) => new InputControlEntitySchema(value);
    protected override getControlEntityConfig = (value: IControlEntityConfig) => new InputControlEntityConfig(value);
}
