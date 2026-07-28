import { GpllApiFormComponent } from '../api-form.component';

export class ApiFormActionResult {
    public model: any;
    public formValue: any;
    public apiForm!: GpllApiFormComponent;
}

export class SubmitApiFormActionResult extends ApiFormActionResult {}
