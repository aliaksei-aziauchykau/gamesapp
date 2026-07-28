import { CommonModule } from '@angular/common';
import {
    ChangeDetectionStrategy,
    Component,
    ContentChild,
    inject,
    Injector,
    Input,
    OnInit,
    Provider,
    TemplateRef,
} from '@angular/core';

@Component({
    selector: '[shrlWithProviders]',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './with-providers.component.html',
    styleUrl: './with-providers.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShrlWithProvidersComponent implements OnInit {
    @Input() public providers!: Provider[];

    @ContentChild(TemplateRef) public content!: TemplateRef<unknown>;

    private readonly parentInjector = inject(Injector);
    public injector!: Injector;

    ngOnInit(): void {
        this.injector = Injector.create({
            providers: this.providers,
            parent: this.parentInjector,
        });
    }
}
