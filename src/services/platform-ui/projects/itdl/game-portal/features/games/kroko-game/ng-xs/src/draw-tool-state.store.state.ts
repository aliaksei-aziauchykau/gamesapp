/* eslint-disable sort-exports/sort-exports */
import { Injectable } from '@angular/core';
import { DrawToolStateActions } from '@itdl/game-portal/features/games/kroko-game/actions';
import { DrawToolState, IDrawToolState } from '@itdl/game-portal/features/games/kroko-game/entities';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { firstValueFrom } from 'rxjs';

export interface IDrawToolStateXSState {
    initialDrawToolState: IDrawToolState;
    pushHistory: (() => void)[][];
    popHistory: (() => void)[][];
}

const defaults: IDrawToolStateXSState = {
    initialDrawToolState: new DrawToolState([]),
    pushHistory: [],
    popHistory: [],
};

@State<IDrawToolStateXSState>({
    name: 'drawToolState',
    defaults,
})
@Injectable()
export class DrawToolStateXSState {
    @Action(DrawToolStateActions.StoreInitialState)
    public storeInitialDrawToolState(
        { patchState }: StateContext<IDrawToolStateXSState>,
        { drawToolState }: DrawToolStateActions.StoreInitialState,
    ) {
        patchState({ initialDrawToolState: drawToolState });
    }

    @Action(DrawToolStateActions.StoreActionToHistory)
    public storeActionToHistory(
        { patchState, getState }: StateContext<IDrawToolStateXSState>,
        { action }: DrawToolStateActions.StoreActionToHistory,
    ) {
        const pushHistory = getState().pushHistory;
        patchState({ pushHistory: [...pushHistory, [action]], popHistory: [] });
    }

    @Action(DrawToolStateActions.StoreActionAsLastToHistory)
    public storeActionAsLastToHistory(
        { patchState, getState }: StateContext<IDrawToolStateXSState>,
        { action }: DrawToolStateActions.StoreActionAsLastToHistory,
    ) {
        const pushHistory = getState().pushHistory;
        const lastActions = pushHistory[pushHistory.length - 1];
        const pushHistoryWithoutLast = pushHistory.slice(0, -1);
        if (!lastActions) return;
        patchState({ pushHistory: [...pushHistoryWithoutLast, [...lastActions, action]], popHistory: [] });
    }

    @Action(DrawToolStateActions.Undo)
    public async undo(
        { dispatch, getState, patchState }: StateContext<IDrawToolStateXSState>,
        { nextStateFn }: DrawToolStateActions.Undo,
    ) {
        nextStateFn(getState().initialDrawToolState);

        const pushHistory = getState().pushHistory;
        if (!pushHistory.length) return;

        const action = pushHistory[pushHistory.length - 1];
        if (!action) return;

        const pushHistoryWithoutLast = pushHistory.slice(0, -1);

        const popHistory = getState().popHistory;

        patchState({ pushHistory: [...pushHistoryWithoutLast], popHistory: [...popHistory, action] });

        await firstValueFrom(dispatch(new DrawToolStateActions.ApplyHistoryActions()));
    }

    @Action(DrawToolStateActions.Redo)
    public async redo(
        { dispatch, getState, patchState }: StateContext<IDrawToolStateXSState>,
        { nextStateFn }: DrawToolStateActions.Redo,
    ) {
        nextStateFn(getState().initialDrawToolState);

        const popHistory = getState().popHistory;
        if (!popHistory.length) return;

        const action = popHistory[popHistory.length - 1];
        if (!action) return;

        const popHistoryWithoutLast = popHistory.slice(0, -1);

        const pushHistory = getState().pushHistory;

        patchState({ pushHistory: [...pushHistory, action], popHistory: [...popHistoryWithoutLast] });

        await firstValueFrom(dispatch(new DrawToolStateActions.ApplyHistoryActions()));
    }

    @Action(DrawToolStateActions.ClearHistory)
    public async clearHistory({ patchState }: StateContext<IDrawToolStateXSState>) {
        patchState({ pushHistory: [], popHistory: [] });
    }

    @Action(DrawToolStateActions.ApplyHistoryActions)
    public async applyHistoryActions({ dispatch, getState }: StateContext<IDrawToolStateXSState>) {
        const pushHistory = getState().pushHistory;

        for (const action of pushHistory.flat()) {
            action();
        }
    }

    @Selector()
    public static isUndoAvailable(drawToolState: IDrawToolStateXSState) {
        return drawToolState.pushHistory.length > 0;
    }

    @Selector()
    public static isRedoAvailable(drawToolState: IDrawToolStateXSState) {
        return drawToolState.popHistory.length > 0;
    }
}
