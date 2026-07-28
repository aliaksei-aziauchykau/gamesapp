import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { HttpService } from './services/http.service';

@NgModule({
    imports: [HttpClientModule],
    exports: [],
    declarations: [],
    providers: [HttpService],
})
export class HttpModule {}
