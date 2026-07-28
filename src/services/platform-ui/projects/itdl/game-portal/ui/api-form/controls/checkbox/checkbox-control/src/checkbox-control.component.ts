import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GpllBaseControlComponent } from '@itdl/game-portal/ui/api-form/controls/base';
import { CheckboxControlEntityConfig } from '@itdl/game-portal/ui/api-form/controls/checkbox/checkbox-config';
import { CheckboxControlEntitySchema } from '@itdl/game-portal/ui/api-form/controls/checkbox/checkbox-schema';
import { IControlEntityConfig, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

@Component({
    selector: 'gpll-checkbox-control',
    templateUrl: './checkbox-control.component.html',
    styleUrls: ['./checkbox-control.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllCheckboxControlComponent extends GpllBaseControlComponent<
    CheckboxControlEntitySchema,
    CheckboxControlEntityConfig,
    boolean
> {
    protected override getControlEntitySchema = (value: IControlEntitySchema) => new CheckboxControlEntitySchema(value);
    protected override getControlEntityConfig = (value: IControlEntityConfig) => new CheckboxControlEntityConfig(value);
}
