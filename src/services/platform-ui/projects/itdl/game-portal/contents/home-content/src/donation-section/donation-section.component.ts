import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { GpllStripeDonateButtonComponent } from '@itdl/game-portal/features/payment/stripe-integration';

@Component({
    selector: 'gpll-donation-section',
    standalone: true,
    imports: [
        CommonModule,
        MatButtonModule,
        MatCardModule,
        MatInputModule,
        ReactiveFormsModule,
        GpllStripeDonateButtonComponent,
    ],
    templateUrl: './donation-section.component.html',
    styleUrls: ['./donation-section.component.scss'],
})
export class GpllDonationSectionComponent {
    donationForm!: FormGroup;

    constructor(private formBuilder: FormBuilder) {
        this.createDonationForm();
    }

    public createDonationForm() {
        this.donationForm = this.formBuilder.group({
            amount: ['', [Validators.required, Validators.min(1)]],
        });
    }

    public submitDonation() {
        if (this.donationForm.valid) {
            const donationAmount = this.donationForm.value.amount;
            console.log(`Donation Amount: ${donationAmount}`);
            // Here, handle the donation logic, such as sending the data to a server
        }
    }
}
