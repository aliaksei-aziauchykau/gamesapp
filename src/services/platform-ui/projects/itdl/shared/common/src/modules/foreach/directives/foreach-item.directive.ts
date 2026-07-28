import { Directive, Input } from '@angular/core';
interface ShrlForeachTemplateContext<T> {
    $implicit: T;
    shrlForeachItem: T;
    index: number;
}

@Directive({ selector: 'ng-template[shrlForeachItem]' })
export class ShrlForeachItemDirective<T> {
    @Input('shrlForeachItem') list!: T[];

    public static ngTemplateContextGuard<TContext>(
        dir: ShrlForeachItemDirective<TContext>,
        ctx: unknown,
    ): ctx is ShrlForeachTemplateContext<TContext> {
        return true;
    }
}
