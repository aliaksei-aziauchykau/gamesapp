import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { DestroyableDirective, WithRequired } from '@itdl/shared/common';
import { fromEvent, merge, switchMap } from 'rxjs';

import { LogicAnchorStoreService } from '../navigation/services/logic-anchor-registration.service';
import { NavigationService } from '../navigation/services/navigation.service';

export type ScrollToLogicAnchorConfig = {
    logicAnchorName?: string;
    eventNames?: string[];
};

@Directive({ selector: '[shrlScrollToLogicAnchor]' })
export class ShrlScrollToLogicAnchorDirective extends DestroyableDirective implements OnInit {
    private _config!: WithRequired<ScrollToLogicAnchorConfig, 'eventNames'>;
    @Input() public set shrlScrollToLogicAnchor(config: ScrollToLogicAnchorConfig) {
        this._config = { eventNames: ['click'], ...config };
    }

    constructor(
        private readonly logicAnchorStoreService: LogicAnchorStoreService,
        private readonly navigationService: NavigationService,
        private readonly elementRef: ElementRef,
    ) {
        super();
    }

    ngOnInit(): void {
        if (!this._config.logicAnchorName) return;
        const logicAnchor = this.logicAnchorStoreService.getEntity(this._config.logicAnchorName);

        if (!logicAnchor) return console.warn('Logic anchor is not found');
        const scrollToElement = logicAnchor.elementRef.nativeElement;

        const events$ = this._config.eventNames.map((eventName) => fromEvent(this.elementRef.nativeElement, eventName));
        merge(...events$)
            .pipe(switchMap(() => this.navigationService.scrollToElement$(scrollToElement)))
            .subscribe();
    }
}
