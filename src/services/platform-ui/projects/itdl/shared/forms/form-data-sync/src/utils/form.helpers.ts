import { AbstractControl, FormArray, FormGroup } from '@angular/forms';

export function visitAllFormControls(group: FormGroup | FormArray, visitor: (control: AbstractControl) => void) {
    visitor(group);
    Object.keys(group.controls).forEach((key: string) => {
        const abstractControl = Object(group.controls)[key] as AbstractControl;

        if (abstractControl instanceof FormGroup || abstractControl instanceof FormArray) {
            visitAllFormControls(abstractControl, visitor);
            return;
        }

        visitor(abstractControl);
    });
}

export function getAllFormControls(group: FormGroup | FormArray): AbstractControl[] {
    const controls: AbstractControl[] = [];
    visitAllFormControls(group, (control) => controls.push(control));

    return controls;
}
