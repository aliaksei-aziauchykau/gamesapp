import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
    selector: 'gpll-news-section',
    templateUrl: './news-section.component.html',
    styleUrls: ['./news-section.component.scss'],
    standalone: true,
    imports: [CommonModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule],
})
export class GpllNewsSectionComponent {}
