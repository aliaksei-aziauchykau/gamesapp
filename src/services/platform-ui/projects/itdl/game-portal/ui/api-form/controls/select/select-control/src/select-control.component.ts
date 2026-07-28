import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GpllBaseControlComponent } from '@itdl/game-portal/ui/api-form/controls/base';
import { SelectControlEntityConfig } from '@itdl/game-portal/ui/api-form/controls/select/select-config';
import { SelectControlEntitySchema } from '@itdl/game-portal/ui/api-form/controls/select/select-schema';
import { IControlEntityConfig, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

@Component({
    selector: 'gpll-select-control',
    templateUrl: './select-control.component.html',
    styleUrls: ['./select-control.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllSelectControlComponent extends GpllBaseControlComponent<
    SelectControlEntitySchema,
    SelectControlEntityConfig,
    string
> {
    protected override getControlEntitySchema = (value: IControlEntitySchema) => new SelectControlEntitySchema(value);
    protected override getControlEntityConfig = (value: IControlEntityConfig) => new SelectControlEntityConfig(value);
}
