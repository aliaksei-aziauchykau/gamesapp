import { CommonModule } from '@angular/common';
import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { IKrokoMessage } from '@itdl/game-portal/features/games/kroko-game/entities';
import { KrokoGameXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { UserXSState } from '@itdl/game-portal/features/user/ng-xs';
import { GpllTranslationModule } from '@itdl/game-portal/translation';
import { GpllInputControlStyleModule } from '@itdl/game-portal/ui/api-form/controls/input/input-control-style';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { Store } from '@ngxs/store';
import { RxState } from '@rx-angular/state';
import { RxLet } from '@rx-angular/template/let';

import { GpllKrokoGameActionsService } from '../../services/kroko-game.actions.service';

type State = {
    krokoMessages: IKrokoMessage[];
    isDrawer: boolean;
};

@Component({
    selector: 'gpll-kroko-chat',
    templateUrl: './kroko-chat.component.html',
    styleUrls: ['./kroko-chat.component.scss'],
    standalone: true,
    imports: [
        CommonModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        GpllTranslationModule,
        GpllInputControlStyleModule,
        RxLet,
    ],
    providers: [RxState],
})
export class GpllKrokoChatComponent {
    @ViewChild('chatContainer') private chatContainer!: ElementRef;

    public readonly krokoMessages$ = this.state.select('krokoMessages');
    public readonly isDrawer$ = this.state.select('isDrawer');

    private readonly uiActions = inject(ShrlUiActionsService);
    private readonly gameActions = inject(GpllKrokoGameActionsService);

    private readonly gameCode = this.store.selectSnapshot(GameXSState.gameCode);
    private readonly userId = this.store.selectSnapshot(UserXSState.userIdSafety);

    private readonly guessWordAction = this.gameActions.getGuessWordActionFn(this.gameCode, this.userId);

    public readonly form = new FormGroup({
        message: new FormControl(''),
    });

    constructor(private readonly state: RxState<State>, private readonly store: Store) {
        this.state.connect('krokoMessages', this.store.select(KrokoGameXSState.currentRoundMessages));
        this.state.connect('isDrawer', this.store.select(KrokoGameXSState.isDrawer));
        this.state.hold(this.krokoMessages$, () => setTimeout(() => this.scrollToBottom()));
        this.state.hold(this.isDrawer$, (isDrawer) => (isDrawer ? this.form.disable() : this.form.enable()));
    }

    public sendMessage(): void {
        if (!this.form.value.message) return;
        this.guessWordAction(this.form.value.message);
        this.form.reset();
    }

    public onEnter(event: Event) {
        const keyboardEvent = event as KeyboardEvent;
        keyboardEvent.preventDefault();
        this.sendMessage();
    }

    private scrollToBottom(): void {
        if (!this.chatContainer) return;
        this.chatContainer.nativeElement.scrollTop = this.chatContainer.nativeElement.scrollHeight;
    }
}
