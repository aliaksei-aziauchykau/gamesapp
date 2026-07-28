import { Component, ContentChild, TemplateRef } from '@angular/core';

@Component({
    selector: 'shrl-container',
    templateUrl: 'container.component.html',
})
export class ContainerComponent {
    @ContentChild('beforeContainerTemplate') beforeContainerTemplate?: TemplateRef<any>;
    @ContentChild('afterContainerTemplate') afterContainerTemplate?: TemplateRef<any>;
}
