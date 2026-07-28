import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { shake } from '@itdl/shared/animations';
import { RxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';
import { filter, interval, map, startWith } from 'rxjs';

@Component({
    selector: 'gpll-countdown-timer',
    templateUrl: './countdown-timer.component.html',
    styleUrls: ['./countdown-timer.component.scss'],
    standalone: true,
    imports: [CommonModule, RxLet],
    providers: [RxState],
    animations: [shake],
})
export class GpllCountdownTimerComponent {
    @Input() date!: Date; // The future time in milliseconds

    public readonly countdownStream$ = interval(1000).pipe(
        map(() => this.date?.getTime() ?? Date.now()),
        map((date) => date - Date.now()),
        filter((timeRemaining) => timeRemaining > 0),
        map((timeRemaining) => ({
            minutes: this.formatTime(Math.floor((timeRemaining / 1000 / 60) % 60)),
            seconds: this.formatTime(Math.floor((timeRemaining / 1000) % 60)),
        })),
        startWith({ minutes: '--', seconds: '--' }),
    );

    public readonly countdown$ = this.state.select('countdown');

    constructor(private readonly state: RxState<{ countdown: { minutes: string; seconds: string } }>) {
        this.state.connect('countdown', this.countdownStream$);
    }

    private formatTime(time: number): string {
        return time < 10 ? `0${time}` : time.toString();
    }
}
