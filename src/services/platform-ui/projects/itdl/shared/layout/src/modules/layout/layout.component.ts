import { CommonModule } from '@angular/common';
import { Component, ContentChild, TemplateRef } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';

import { ShrlAfterContentTemplateModule } from './modules/after-content-template/after-content-template.module';
import { ShrlAfterContentTemplateReactiveValue } from './modules/after-content-template/after-content-template.reactive-value.service';
import { ShrlBeforeContentTemplateModule } from './modules/before-content-template/before-content-template.module';
import { ShrlBeforeContentTemplateReactiveValue } from './modules/before-content-template/before-content-template.reactive-value.service';
import { ShrlFooterTemplateModule } from './modules/footer-template/footer-template.module';
import { ShrlFooterTemplateReactiveValue } from './modules/footer-template/footer-template.reactive-value.service';
import { ShrlHeaderTemplateModule } from './modules/header-template/header-template.module';
import { ShrlHeaderTemplateReactiveValue } from './modules/header-template/header-template.reactive-value.service';

@Component({
    selector: 'shrl-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss'],
    standalone: true,
    imports: [
        CommonModule,
        MatSidenavModule,
        ShrlHeaderTemplateModule,
        ShrlBeforeContentTemplateModule,
        ShrlAfterContentTemplateModule,
        ShrlFooterTemplateModule,
    ],
})
export class ShrlLayoutComponent {
    @ContentChild('headerTemplate') headerTemplate?: TemplateRef<unknown>;
    @ContentChild('beforeContentTemplate') beforeContentTemplate?: TemplateRef<unknown>;
    @ContentChild('afterContentTemplate') afterContentTemplate?: TemplateRef<unknown>;
    @ContentChild('footerTemplate') footerTemplate?: TemplateRef<unknown>;

    public readonly headerTemplate$ = this.headerTemplateReactiveValue.value$;
    public readonly beforeContentTemplate$ = this.beforeContentTemplateReactiveValue.value$;
    public readonly afterContentTemplate$ = this.afterContentTemplateReactiveValue.value$;
    public readonly footerTemplate$ = this.footerTemplateReactiveValue.value$;

    constructor(
        private readonly headerTemplateReactiveValue: ShrlHeaderTemplateReactiveValue,
        private readonly beforeContentTemplateReactiveValue: ShrlBeforeContentTemplateReactiveValue,
        private readonly afterContentTemplateReactiveValue: ShrlAfterContentTemplateReactiveValue,
        private readonly footerTemplateReactiveValue: ShrlFooterTemplateReactiveValue,
    ) {}
}
