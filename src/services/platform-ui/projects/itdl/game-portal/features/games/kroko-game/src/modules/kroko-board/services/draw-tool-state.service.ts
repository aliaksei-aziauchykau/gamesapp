/* eslint-disable sort-exports/sort-exports */
import { inject, Injectable } from '@angular/core';
import { DrawToolStateActions } from '@itdl/game-portal/features/games/kroko-game/actions';
import { DrawShapeConfig, DrawToolState, IDrawToolState } from '@itdl/game-portal/features/games/kroko-game/entities';
import { DrawToolStateXSState } from '@itdl/game-portal/features/games/kroko-game/ng-xs';
import { ReactiveValue } from '@itdl/shared/common';
import { Store } from '@ngxs/store';

@Injectable()
export class GpllDrawToolStateService extends ReactiveValue<IDrawToolState> {
    private readonly store = inject(Store);

    public isUndoAvailable$ = this.store.select(DrawToolStateXSState.isUndoAvailable);
    public isRedoAvailable$ = this.store.select(DrawToolStateXSState.isRedoAvailable);

    constructor() {
        super(new DrawToolState());
        this.store.dispatch(new DrawToolStateActions.ClearHistory());
    }

    public get shapes(): DrawShapeConfig[] {
        return this.value?.shapes || [];
    }

    public addShape(shape: DrawShapeConfig) {
        const action = () => {
            const shapes = this.shapes;
            this.next({ shapes: [...shapes, shape] });
        };
        action();
        return new DrawToolAction(action, this.store);
    }

    public replaceLastShape(shape: DrawShapeConfig) {
        const action = () => {
            const shapes = this.shapes;
            shapes.splice(shapes.length - 1, 1, { ...shape });
            this.next({ shapes: [...shapes] });
        };
        action();

        return new DrawToolAction(action, this.store);
    }

    public clear() {
        const action = () => {
            this.next({ shapes: [] });
        };
        action();

        return new DrawToolAction(action, this.store);
    }

    public undo() {
        this.store.dispatch(new DrawToolStateActions.Undo((state) => this.next(state)));
    }

    public redo() {
        this.store.dispatch(new DrawToolStateActions.Redo((state) => this.next(state)));
    }
}

export class DrawToolAction {
    constructor(private readonly action: () => void, private readonly store: Store) {}

    public addToHistory() {
        this.store.dispatch(new DrawToolStateActions.StoreActionToHistory(this.action));
    }

    public addToAsLastHistory() {
        this.store.dispatch(new DrawToolStateActions.StoreActionAsLastToHistory(this.action));
    }
}

// private readonly store = inject(Store);

// constructor() {
//     this.store.dispatch(new DrawToolStateActions.NewState(new DrawToolState()));
// }

// public isUndoAvailable$ = this.store.select(DrawToolStateXSState.isUndoAvailable);
// public isRedoAvailable$ = this.store.select(DrawToolStateXSState.isRedoAvailable);

// public get shapes(): DrawShapeConfig[] {
//     return this.store.selectSnapshot(DrawToolStateXSState.shapes) || [];
// }

// public get value() {
//     return this.store.selectSnapshot(DrawToolStateXSState.state);
// }

// public get valueSafe() {
//     if (this.value === null) throw new Error('ReactiveValue is null');
//     return this.value;
// }

// public get value$() {
//     return this.store.select(DrawToolStateXSState.state);
// }

// public next(state: IDrawToolState) {
//     this.store.dispatch(new DrawToolStateActions.NewState(state));
// }

// public addShape(shape: DrawShapeConfig) {
//     const action = new DrawToolStateActions.AddShape(shape);
//     this.store.dispatch(action);
//     return new DrawToolAction(action, this.store);
// }

// public replaceLastShapeOnMove(shape: DrawShapeConfig) {
//     const state = this.valueSafe;
//     const shapes = state.shapes;
//     if (!shapes) return;
//     shapes.pop();
//     state.shapes = [...shapes, shape];
// }

// public replaceLastShape(shape: DrawShapeConfig) {
//     const action = new DrawToolStateActions.ReplaceLastShape(shape);
//     this.store.dispatch(action);
//     return new DrawToolAction(action, this.store);
// }

// public clear() {
//     const action = new DrawToolStateActions.ClearShapes();
//     this.store.dispatch(action);
//     return new DrawToolAction(action, this.store);
// }

// public undo() {
//     const action = new DrawToolStateActions.Undo();
//     this.store.dispatch(action);
//     return new DrawToolAction(action, this.store);
// }

// public redo() {
//     const action = new DrawToolStateActions.Redo();
//     this.store.dispatch(action);
//     return new DrawToolAction(action, this.store);
// }
