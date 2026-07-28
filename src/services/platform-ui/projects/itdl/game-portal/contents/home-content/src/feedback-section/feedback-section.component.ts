import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
    selector: 'gpll-feedback-section',
    standalone: true,
    imports: [CommonModule, MatFormFieldModule, MatInputModule, MatButtonModule],
    templateUrl: './feedback-section.component.html',
    styleUrls: ['./feedback-section.component.scss'],
})
export class GpllFeedbackSectionComponent {}
