import { Component, ContentChild, TemplateRef } from '@angular/core';

@Component({
    selector: 'shrl-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss'],
})
export class FooterComponent {
    @ContentChild('footerContentTemplate') footerContentTemplate?: TemplateRef<any>;
}
