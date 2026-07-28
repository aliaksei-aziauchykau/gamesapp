import {
    ComponentRef,
    Directive,
    Inject,
    Input,
    OnInit,
    Optional,
    TemplateRef,
    Type,
    ViewContainerRef,
} from '@angular/core';
import { isServerPlatformToken } from '@itdl/shared/common';

import { ShrlServerLoaderComponentInputsToken, ShrlServerLoaderComponentTypeToken } from './server-loader.token';

export interface IShrlServerLoaderConfig {
    componentType?: Type<unknown>;
    componentInputs?: Record<string, unknown>;
}

@Directive({ selector: '[shrlServerLoader]' })
export class ShrlServerLoaderDirective implements OnInit {
    @Input() shrlServerLoader: IShrlServerLoaderConfig | '' = {};

    private hasView = false;

    constructor(
        private templateRef: TemplateRef<unknown>,
        private viewContainer: ViewContainerRef,
        @Inject(isServerPlatformToken) private readonly isServerPlatform: boolean,
        @Optional()
        @Inject(ShrlServerLoaderComponentTypeToken)
        private readonly serverLoaderComponentType: Type<unknown>,
        @Optional()
        @Inject(ShrlServerLoaderComponentInputsToken)
        private readonly serverLoaderComponentInputs: Record<string, unknown>,
    ) {}

    ngOnInit(): void {
        const inputs = this.shrlServerLoader === '' ? {} : this.shrlServerLoader;
        const { componentType, componentInputs } = inputs;

        if (!this.isServerPlatform && !this.hasView) {
            this.viewContainer.createEmbeddedView(this.templateRef);
            this.hasView = true;
        } else if (this.isServerPlatform && !this.hasView) {
            const componentRef = this.viewContainer.createComponent(componentType ?? this.serverLoaderComponentType);
            if (componentInputs) this.setInputs(componentRef, componentInputs ?? this.serverLoaderComponentInputs);
            this.hasView = true;
        }
    }

    private setInputs(ref: ComponentRef<unknown>, inputs: Record<string, unknown>) {
        Object.entries(inputs).forEach(([key, value]) => {
            ref.setInput(key, value);
        });
    }
}
