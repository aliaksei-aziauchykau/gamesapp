import { ChangeDetectionStrategy, Component, inject, Injector, Input } from '@angular/core';
import { IControlEntitySchema } from '@itdl/game-portal/ui/api-form/entities';

@Component({
    selector: 'gpll-control-loader',
    templateUrl: './control-loader.component.html',
    styleUrls: ['./control-loader.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GpllControlLoaderComponent {
    @Input() schema!: IControlEntitySchema;
    @Input() value!: unknown | undefined;

    public readonly injector = inject(Injector);
}
