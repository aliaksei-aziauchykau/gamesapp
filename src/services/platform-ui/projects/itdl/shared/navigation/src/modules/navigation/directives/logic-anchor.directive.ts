import { Directive, ElementRef, Input, OnInit } from '@angular/core';

import { IElementRefContainer } from '../models/element-ref-container.interface';
import { LogicAnchorStoreService } from '../services/logic-anchor-registration.service';

@Directive({ selector: '[shrlLogicAnchor]' })
export class ShrlLogicAnchorDirective implements OnInit, IElementRefContainer {
    @Input() shrlLogicAnchor!: string;
    @Input() nextLogicAnchor!: string;
    @Input() previousLogicAnchor!: string;

    constructor(
        public readonly elementRef: ElementRef<HTMLElement>,
        private readonly logicAnchorStoreService: LogicAnchorStoreService,
    ) {}

    ngOnInit(): void {
        this.logicAnchorStoreService.registerEntity({ key: this.shrlLogicAnchor, entity: this });
    }
}
