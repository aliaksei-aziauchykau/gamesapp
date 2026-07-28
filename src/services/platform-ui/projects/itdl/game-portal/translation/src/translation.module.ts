import { NgModule } from '@angular/core';

import { GpllTranslatePipe } from './pipes/translate.pipe';
import { GpllTranslateService } from './services/translate.service';

@NgModule({
    imports: [],
    declarations: [GpllTranslatePipe],
    exports: [GpllTranslatePipe],
    providers: [GpllTranslateService],
})
export class GpllTranslationModule {}
