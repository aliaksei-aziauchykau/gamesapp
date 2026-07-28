import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ShrlFetcherComponent } from './fetcher.component';

@NgModule({
    imports: [CommonModule],
    exports: [ShrlFetcherComponent],
    declarations: [ShrlFetcherComponent],
    providers: [],
})
export class ShrlFetcherModule {}
