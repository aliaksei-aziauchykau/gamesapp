import { Directive, HostListener, inject, Input, OnInit } from '@angular/core';
import { IDrawToolEventHandler } from '@itdl/game-portal/features/games/kroko-game/entities';
import { ShrlKonvaStageComponent } from '@itdl/shared/ui/konva';

import { GpllDrawToolStageService } from '../services/draw-tool-stage.service';

@Directive({ selector: '[gpllDrawTool]', standalone: true })
export class GpllDrawToolDirective implements OnInit {
    @Input() gpllDrawTool!: IDrawToolEventHandler | null;

    private readonly stageComponent = inject(ShrlKonvaStageComponent);
    private readonly drawToolStageService = inject(GpllDrawToolStageService);

    public ngOnInit(): void {
        this.drawToolStageService.next(this.stageComponent.container);
    }

    @HostListener('mousemove', ['$event'])
    public onMouseMove(event: MouseEvent): void {
        this.gpllDrawTool?.onMouseMove(event);
    }

    @HostListener('mousedown', ['$event'])
    public onMouseDown(event: MouseEvent): void {
        this.gpllDrawTool?.onMouseDown(event);
    }

    @HostListener('mouseup', ['$event.target'])
    public onMouseUp(event: MouseEvent): void {
        this.gpllDrawTool?.onMouseUp(event);
    }

    @HostListener('touchstart', ['$event'])
    public onTouchStart(event: TouchEvent): void {
        this.gpllDrawTool?.onTouchStart(event);
    }

    @HostListener('touchmove', ['$event'])
    public onTouchMove(event: TouchEvent): void {
        this.gpllDrawTool?.onTouchMove(event);
    }

    @HostListener('touchend', ['$event'])
    public onTouchEnd(event: TouchEvent): void {
        this.gpllDrawTool?.onTouchEnd(event);
    }
}
