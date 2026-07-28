import {
    AfterContentChecked,
    AfterContentInit,
    AfterViewChecked,
    AfterViewInit,
    Directive,
    Input,
    OnChanges,
    OnDestroy,
    OnInit,
    SimpleChanges,
} from '@angular/core';
import { Dictionary } from '@itdl/shared/common';

import { ShrlLifecycleHooks } from './lifecycle-hooks.enum';

type ShrlLifecycleHookCallback = () => void;

@Directive({ selector: '[shrlLifecycleHook]', standalone: true })
export class ShrlLifecycleHookDirective
    implements OnInit, OnDestroy, OnChanges, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked
{
    @Input() shrlLifecycleHook: Dictionary<ShrlLifecycleHooks, ShrlLifecycleHookCallback> = {};

    public ngOnInit(): void {
        if (this.shrlLifecycleHook[ShrlLifecycleHooks.OnInit]) {
            this.shrlLifecycleHook[ShrlLifecycleHooks.OnInit]();
        }
    }

    public ngOnDestroy(): void {
        if (this.shrlLifecycleHook[ShrlLifecycleHooks.OnDestroy]) {
            this.shrlLifecycleHook[ShrlLifecycleHooks.OnDestroy]();
        }
    }

    public ngOnChanges(_: SimpleChanges): void {
        if (this.shrlLifecycleHook[ShrlLifecycleHooks.OnChanges]) {
            this.shrlLifecycleHook[ShrlLifecycleHooks.OnChanges]();
        }
    }

    public ngAfterContentInit(): void {
        if (this.shrlLifecycleHook[ShrlLifecycleHooks.AfterContentInit]) {
            this.shrlLifecycleHook[ShrlLifecycleHooks.AfterContentInit]();
        }
    }

    public ngAfterContentChecked(): void {
        if (this.shrlLifecycleHook[ShrlLifecycleHooks.AfterViewChecked]) {
            this.shrlLifecycleHook[ShrlLifecycleHooks.AfterViewChecked]();
        }
    }

    public ngAfterViewInit(): void {
        if (this.shrlLifecycleHook[ShrlLifecycleHooks.AfterViewInit]) {
            this.shrlLifecycleHook[ShrlLifecycleHooks.AfterViewInit]();
        }
    }

    public ngAfterViewChecked(): void {
        if (this.shrlLifecycleHook[ShrlLifecycleHooks.AfterViewChecked]) {
            this.shrlLifecycleHook[ShrlLifecycleHooks.AfterViewChecked]();
        }
    }
}
