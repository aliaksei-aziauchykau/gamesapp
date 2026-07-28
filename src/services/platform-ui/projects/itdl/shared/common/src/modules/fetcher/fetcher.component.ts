import { ChangeDetectionStrategy, Component, ContentChild, Input, TemplateRef } from '@angular/core';
import { RxState } from '@rx-angular/state';
import { Observable } from 'rxjs';

@Component({
    selector: 'shrl-fetcher',
    templateUrl: './fetcher.component.html',
    styleUrls: ['./fetcher.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [RxState],
})
export class ShrlFetcherComponent {
    @Input() public dataSource$!: Observable<unknown>;
    @ContentChild(TemplateRef) public content!: TemplateRef<unknown>;

    public readonly data$ = this.state.select('data');

    constructor(private readonly state: RxState<{ data: unknown }>) {}

    public ngOnInit(): void {
        this.state.connect('data', this.dataSource$);
    }
}
