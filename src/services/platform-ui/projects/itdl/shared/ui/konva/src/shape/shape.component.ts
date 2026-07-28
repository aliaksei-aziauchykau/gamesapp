import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Shape, ShapeConfig } from 'konva/lib/Shape';

import { ContainerContextService } from '../container-context.service';

@Component({ template: '' })
export abstract class ShrlKonvaShapeComponent<TShape extends Shape, TShapeConfig extends ShapeConfig>
    implements OnInit, OnDestroy
{
    private _config!: TShapeConfig;

    @Input() public set config(value: TShapeConfig) {
        this._config = value;
        if (!this.shape) return;
        this.shape.setAttrs(value);
        this.shape.getLayer()?.batchDraw();
    }

    public get config(): TShapeConfig {
        return this._config;
    }

    private shape!: TShape;

    constructor(
        protected readonly element: HTMLElement,
        protected readonly parentContainerContext: ContainerContextService,
    ) {}

    protected abstract createShape(config: TShapeConfig, element: HTMLElement): TShape;

    public ngOnInit(): void {
        this.shape = this.createShape(this.config, this.element);
        this.parentContainerContext.value?.add(this.shape as any);
    }

    public ngOnDestroy(): void {
        this.shape.destroy();
    }
}
