import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { IDrawToolContext } from '@itdl/game-portal/features/games/kroko-game/entities';
import { Layer } from 'konva/lib/Layer';
import { Stage } from 'konva/lib/Stage';

type State = {
    context: IDrawToolContext;
    isDrawer: boolean;
    currentRoundShapesParsed: any[];
};

@Component({
    selector: 'gpll-test-canvas',
    templateUrl: './test-canvas.component.html',
    standalone: true,
    imports: [CommonModule],
})
export class GpllTestCanvasComponent implements OnInit, AfterViewInit {
    @Input() public clientWidth!: number;
    @Input() public clientHeight!: number;

    @ViewChild('cont') private readonly cont!: ElementRef;

    ngOnInit(): void {}

    ngAfterViewInit(): void {
        var stage = new Stage({
            container: this.cont.nativeElement,
            width: window.innerWidth,
            height: window.innerHeight,
            draggable: false,
        });

        // add canvas element
        const layer = new Layer();
        stage.add(layer);

        // create shape
        // const box = new Rect({
        //     x: 50,
        //     y: 50,
        //     width: 100,
        //     height: 50,
        //     radius: 50,
        //     fill: '#00D2FF',
        //     stroke: 'black',
        //     strokeWidth: 4,
        //     draggable: true,
        // });
        // layer.add(box);

        // add cursor styling
        //     box.on('mouseover', function () {
        //         document.body.style.cursor = 'pointer';
        //     });
        //     box.on('mouseout', function () {
        //         document.body.style.cursor = 'default';
        //     });
        // }
        layer.draw();
    }
}
