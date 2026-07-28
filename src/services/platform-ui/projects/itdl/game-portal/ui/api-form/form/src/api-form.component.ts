import {
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    inject,
    Input,
    OnDestroy,
    OnInit,
    Output,
    ViewEncapsulation,
} from '@angular/core';
import { GpllFormGroup } from '@itdl/game-portal/forms';
import { IControlEntitySchema, IFormEntitySchema } from '@itdl/game-portal/ui/api-form/entities';
import { rxActions } from '@rx-angular/state/actions';

import { ReactiveValue } from '@itdl/shared/common';
import { rxState } from '@rx-angular/state';
import { map, switchMap } from 'rxjs';
import { ApiFormConfig } from './models/api-form-config';
import { SubmitApiFormActionResult } from './models/submit-action.model';
import { GpllApiFormRefService } from './services/api-form-ref.service';
import { GpllApiFormCancelActionServiceToken } from './tokens/api-form-cancel-action.service.token';
import { GpllApiFormSubmitActionServiceToken } from './tokens/api-form-submit-action.service.token';

type Actions = {
    submit: { entity: unknown };
    cancel: void;
};

type State = {
    spinnerState: SpinnerState;
    config: ApiFormConfig;
};

@Component({
    selector: 'gpll-api-form',
    templateUrl: './api-form.component.html',
    styleUrls: ['./api-form.component.scss'],
    providers: [GpllApiFormRefService],
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation: ViewEncapsulation.None,
})
export class GpllApiFormComponent implements OnInit, OnDestroy {
    @Input() set config(value: ApiFormConfig | undefined) {
        this.state.set({ config: { ...this.defaultConfig, ...value } });
    }

    @Input() schema!: IFormEntitySchema;
    @Input() model: { [key: string]: unknown } = {};

    @Output() submitEvent = new EventEmitter<SubmitApiFormActionResult>();
    @Output() cancelEvent = new EventEmitter<void>();

    public form!: GpllFormGroup;

    public schemas: IControlEntitySchema[] = [];

    private readonly defaultConfig: ApiFormConfig = {
        cancelButtonConfig: { name: 'Cancel' },
        submitButtonConfig: { name: 'Submit' },
    };

    public readonly actions = rxActions<Actions>();
    readonly state = rxState<State>(({ set }) => set({ spinnerState: new SpinnerState(), config: this.defaultConfig }));

    private readonly submitActionService = inject(GpllApiFormSubmitActionServiceToken);
    private readonly cancelActionService = inject(GpllApiFormCancelActionServiceToken);
    private readonly apiFormRefService = inject(GpllApiFormRefService);

    public readonly isLoading$ = this.state.select('spinnerState').pipe(
        switchMap((state) => state.value$),
        map((state) => state === SpinnerStateType.Loading),
    );

    public readonly config$ = this.state.select('config');

    public showSpinner() {
        this.state.get('spinnerState').show();
    }

    public hideSpinner() {
        this.state.get('spinnerState').hide();
    }

    private readonly _submitAction$ = this.actions.onSubmit(
        (stream) => stream,
        (formValue) => {
            this.submitActionService.next({ model: this.model, formValue, apiForm: this });
            this.submitEvent.emit();
        },
    );

    private readonly _cancelAction$ = this.actions.onCancel(
        (stream) => stream,
        () => {
            this.cancelActionService.next(void 0);
            this.cancelEvent.emit();
        },
    );

    ngOnInit(): void {
        this.apiFormRefService.next(this);
        this.schemas = Object.values(this.schema.controls) as IControlEntitySchema[];
        this.form = GpllFormGroup.create({ controls: {} });
    }

    ngOnDestroy(): void {
        this.apiFormRefService.next(null);
    }
}

enum SpinnerStateType {
    Idle,
    Loading,
}

class SpinnerState extends ReactiveValue<SpinnerStateType> {
    constructor() {
        super(SpinnerStateType.Idle);
    }

    public show() {
        this.set(SpinnerStateType.Loading);
    }

    public hide() {
        this.set(SpinnerStateType.Idle);
    }
}
