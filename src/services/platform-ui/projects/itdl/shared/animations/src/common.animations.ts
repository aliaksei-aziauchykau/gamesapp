import { animate, keyframes, state, style, transition, trigger } from '@angular/animations';

export const fade = trigger('fade', [
    transition(':enter', [
        style({
            opacity: 0,
        }),
        animate('1s', style({ opacity: 1 })),
    ]),
    transition(':leave', [animate('1s', style({ opacity: 0 }))]),
]);

export const shake = trigger('shake', [
    state('shake', style({ transform: 'rotate(0deg)' })),
    transition(':enter', [
        style({ transform: 'rotate(10deg) scale(1.2)' }),
        animate(
            '2s linear',
            keyframes([
                style({ transform: 'rotate(-10deg) scale(0.8)', offset: 0.2 }),
                style({ transform: 'rotate(10deg) scale(1.2)', offset: 0.4 }),
                style({ transform: 'rotate(-10deg) scale(0.8)', offset: 0.6 }),
                style({ transform: 'rotate(10deg) scale(1.2)', offset: 0.8 }),
                style({ transform: 'rotate(0deg) scale(1)', offset: 1 }),
            ]),
        ),
    ]),
]);

export const detailExpand = trigger('detailExpand', [
    state('collapsed', style({ height: '0px', minHeight: '0' })),
    state('expanded', style({ height: '*' })),
    transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
]);
