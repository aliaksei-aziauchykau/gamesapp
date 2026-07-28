import {
    ChangeDetectionStrategy,
    Component,
    ComponentRef,
    Input,
    NgModuleRef,
    OnDestroy,
    OnInit,
    Type,
    ViewChild,
    ViewContainerRef,
} from '@angular/core';
import { DestroyableComponent } from '@itdl/shared/common';
import { RxState } from '@rx-angular/state';
import { filter, map, switchMap, takeUntil, tap } from 'rxjs';

import { IDynamicRenderConfiguration } from '../../interfaces/dynamic-render-configuration.interface';
import { DynamicBootstrapComponent } from '../../models/dynamic-bootstrap-component.model';
import { ShrlDynamicLoaderService } from '../../services/dynamic-loader.service';

@Component({
    selector: 'shrl-dynamic-component-loader',
    templateUrl: './dynamic-component-loader.component.html',
    styleUrls: ['./dynamic-component-loader.component.scss'],
    providers: [RxState],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShrlDynamicComponentLoaderComponent extends DestroyableComponent implements OnInit, OnDestroy {
    @Input() public set config(configuration: IDynamicRenderConfiguration) {
        this.state.set(configuration);
    }

    @ViewChild('container', { read: ViewContainerRef, static: true }) container!: ViewContainerRef;
    componentRef!: ComponentRef<DynamicBootstrapComponent>;

    moduleMeta$ = this.state.select().pipe(
        filter((config) => !!config && !!config.alias),
        switchMap((config) =>
            this.dynamicLoaderService
                .getDynamicModuleRefAndComponent(config.alias)
                .pipe(map((moduleMeta) => ({ moduleMeta, config }))),
        ),
        tap(({ moduleMeta, config }) => this.renderView(moduleMeta as any, config)),
        takeUntil(this.onDestroy),
    );

    constructor(
        private readonly state: RxState<IDynamicRenderConfiguration>,
        private readonly dynamicLoaderService: ShrlDynamicLoaderService,
    ) {
        super();
    }

    public ngOnInit(): void {
        this.moduleMeta$.subscribe();
    }

    public override ngOnDestroy() {
        this.componentRef?.destroy();
        super.ngOnDestroy();
    }

    public renderView(
        moduleMeta: {
            module: Type<unknown>;
            moduleRef: NgModuleRef<unknown>;
            entity: Type<DynamicBootstrapComponent>;
        },
        config: IDynamicRenderConfiguration,
    ) {
        // if (!this.container) return setTimeout(() => this.renderView());

        // If container element already is not connected to DOM
        // then the component should not be rendered
        if (!this.container.element.nativeElement.isConnected) return;

        try {
            this.container.clear();
            this.componentRef = this.container.createComponent(moduleMeta.entity, {
                ngModuleRef: moduleMeta.moduleRef,
                injector: moduleMeta.moduleRef.injector,
            });

            const inputs = config.inputs;

            if (inputs) {
                const pairs = Object.entries(inputs).filter((pair) => pair[1] !== undefined);
                pairs.forEach((pair) => (this.componentRef.instance[pair[0]] = pair[1]));
            }

            // this.componentRef.changeDetectorRef.detectChanges();
            this.componentRef.changeDetectorRef.markForCheck();
        } catch (error) {
            // eslint-disable-next-line no-console
            console.error('Something went wrong', error);
        }

        return void 0;
    }
}
