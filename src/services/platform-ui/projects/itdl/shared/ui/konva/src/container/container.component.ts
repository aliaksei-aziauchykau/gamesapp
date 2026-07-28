import { Component, Input, OnInit } from '@angular/core';
import { ContainerConfig } from 'konva/lib/Container';
import { Stage } from 'konva/lib/Stage';

import { ContainerContextService, ContainerType } from '../container-context.service';

@Component({ template: '' })
export abstract class ShrlKonvaContainerComponent<
    TContainer extends ContainerType,
    TContainerConfig extends ContainerConfig,
> implements OnInit
{
    @Input() config!: TContainerConfig;

    public container!: TContainer;

    constructor(
        protected readonly element: HTMLElement,
        protected readonly currentContainerContext: ContainerContextService,
        protected readonly parentContainerContext?: ContainerContextService,
    ) {}

    protected abstract createContainer(config: TContainerConfig, element?: HTMLElement): TContainer;

    public ngOnInit(): void {
        this.container = this.createContainer(this.config, this.element);
        this.currentContainerContext.next(this.container);
        if (this.container instanceof Stage) return;
        this.parentContainerContext?.value?.add(this.container as any);
    }
}
