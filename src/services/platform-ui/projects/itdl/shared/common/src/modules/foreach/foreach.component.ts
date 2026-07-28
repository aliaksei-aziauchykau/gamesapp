import { ChangeDetectionStrategy, Component, ContentChild, Input, TemplateRef } from '@angular/core';

@Component({
    selector: 'shrl-foreach',
    templateUrl: './foreach.component.html',
    styleUrls: ['./foreach.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShrlForeachComponent<T> {
    @Input() public items!: T[];
    @Input() template?: TemplateRef<{ $implicit: T }>;
    @ContentChild(TemplateRef) public content!: TemplateRef<{ $implicit: T }>;
}
