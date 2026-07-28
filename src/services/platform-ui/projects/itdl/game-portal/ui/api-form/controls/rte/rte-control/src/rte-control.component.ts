import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GpllBaseControlComponent } from '@itdl/game-portal/ui/api-form/controls/base';
import { RteControlEntityConfig } from '@itdl/game-portal/ui/api-form/controls/rte/rte-config';
import { RteControlEntitySchema } from '@itdl/game-portal/ui/api-form/controls/rte/rte-schema';
import { IControlEntityConfig, IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

@Component({
    selector: 'gpll-rte-control',
    templateUrl: './rte-control.component.html',
    styleUrls: ['./rte-control.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    // providers: [{ provide: GpllControlConfigsToken, useValue: <IControlEntityConfig>{ isDebug: true }, multi: true }],
})
export class GpllIRteControlComponent extends GpllBaseControlComponent<
    RteControlEntitySchema,
    RteControlEntityConfig,
    string
> {
    protected override getControlEntitySchema = (value: IControlEntitySchema) => new RteControlEntitySchema(value);
    protected override getControlEntityConfig = (value: IControlEntityConfig) => new RteControlEntityConfig(value);
}
