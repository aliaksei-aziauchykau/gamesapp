import { Component, Input, TemplateRef } from '@angular/core';
import { SubmitApiFormActionResult } from '@itdl/game-portal/ui/api-form/form';

type State = {
    submitApiFormActionResult: SubmitApiFormActionResult;
};

@Component({
    selector: 'gpll-grid-view',
    templateUrl: './grid-view.component.html',
    styleUrls: ['./grid-view.component.scss'],
})
export class GpllGridViewComponent {
    @Input() data: unknown[] = [];
    @Input() content!: TemplateRef<any>;
}
