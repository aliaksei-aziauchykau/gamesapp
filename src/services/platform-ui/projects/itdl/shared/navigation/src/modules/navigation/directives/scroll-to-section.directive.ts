import { DOCUMENT } from '@angular/common';
import { AfterViewInit, Directive, Inject, OnDestroy } from '@angular/core';
import { DestroyableDirective } from '@itdl/shared/common';
import {
    debounceTime,
    distinctUntilChanged,
    filter,
    from,
    fromEvent,
    map,
    mergeMap,
    Subject,
    takeUntil,
    tap,
} from 'rxjs';

import { LogicAnchorStoreService } from '../services/logic-anchor-registration.service';
import { scrollIntoViewPromise } from '../utils/scroll-into-view.promise';

@Directive({ selector: '[shrlScrollToSection]' })
export class ShrlScrollToSectionDirective extends DestroyableDirective implements AfterViewInit, OnDestroy {
    private currentMouseElement!: Element | null;
    private lastSt!: number;
    private scrollDirection!: boolean;
    private readonly elementToScroll$ = new Subject<HTMLElement>();

    constructor(
        @Inject(DOCUMENT) public readonly document: Document,
        private readonly logicAnchorStoreService: LogicAnchorStoreService,
    ) {
        super();
    }

    ngAfterViewInit(): void {
        fromEvent(this.document, 'mousemove', (event) => event as MouseEvent)
            .pipe(
                takeUntil(this.onDestroy),
                debounceTime(100),
                tap((event) => console.log(event)),
                map((event) => this.document.elementFromPoint(event.clientX, event.clientY)),
                tap((element) => (this.currentMouseElement = element)),
            )
            .subscribe();

        fromEvent(this.document, 'scroll')
            .pipe(
                takeUntil(this.onDestroy),
                tap(() => this.detectScrollDirection()),
                debounceTime(100),
                map((event) => this.getElementToScroll()),
                tap((element) => console.log('scroll', element)),
                filter((element) => !!element),
                tap((element) => this.elementToScroll$.next(element as HTMLElement)),
            )
            .subscribe();

        this.elementToScroll$
            .pipe(
                takeUntil(this.onDestroy),
                distinctUntilChanged(),
                tap((element) => console.log('go')),
                mergeMap((element) => this.scrollToElement(element as HTMLElement)),
            )
            .subscribe();
    }

    private getElementToScroll() {
        const logicAnchor = this.logicAnchorStoreService
            .getEntities()
            .find((anchor) => anchor.elementRef.nativeElement.contains(this.currentMouseElement));

        if (!logicAnchor) return;

        const elementToScroll = this.logicAnchorStoreService.getEntity(
            this.scrollDirection ? logicAnchor.nextLogicAnchor : logicAnchor.previousLogicAnchor,
        );

        if (!elementToScroll) return;

        return elementToScroll.elementRef.nativeElement;
    }

    private scrollToElement(element: HTMLElement) {
        return from(scrollIntoViewPromise(element, { behavior: 'smooth' }));
    }

    private detectScrollDirection() {
        const st = this.document.defaultView?.pageYOffset || this.document.documentElement.scrollTop;
        this.scrollDirection = st > this.lastSt;
        this.lastSt = st <= 0 ? 0 : st;
    }
}
