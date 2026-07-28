import { Component, Input, OnInit } from '@angular/core';

type BorderAnimationSettings = {
    upLineColor: string;
    downLineColor: string;
    containerBackgroundColor: string;
};

@Component({
    selector: 'gpll-card',
    templateUrl: 'card.component.html',
    styleUrls: ['card.component.scss'],
})
export class GpllCardComponent implements OnInit {
    @Input() public useBorderAnimation = false;
    @Input() public borderAnimationSettings: BorderAnimationSettings = {
        upLineColor: '#cc3571',
        downLineColor: '#e34214',
        containerBackgroundColor: '#000000bf',
    };

    public borderAnimationVariables!: string;

    public ngOnInit(): void {
        this.borderAnimationVariables = this.getBorderAnimationVariables();
    }

    public getBorderAnimationVariables() {
        return [
            `--up-line-color: ${this.borderAnimationSettings.upLineColor}`,
            `--down-line-color: ${this.borderAnimationSettings.downLineColor}`,
            `--container-background-color: ${this.borderAnimationSettings.containerBackgroundColor}`,
        ].join(';');
    }
}
