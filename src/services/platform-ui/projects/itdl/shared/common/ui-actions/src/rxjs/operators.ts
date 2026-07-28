/* eslint-disable sort-exports/sort-exports */
// import { pipe, tap } from 'rxjs';

import { filter, Observable, pipe, tap } from 'rxjs';

import { UiActionExecutionInfo, UiActionMetaInfo } from '../models/models';

// import { UiAction } from '../models/ui-action.model';
// import { completeUiCommand } from './complete-ui-command.operator';
// import { filterUiActions } from './filter-ui-actions.operator';

// export const handleAction = <TI extends UiAction>(uiActionType: { new (...args: any[]): TI; type: string }) => {
//     const operator = pipe(filterUiActions(uiActionType), tap((a) => ), completeUiCommand());

//     return operator;
// };

export const processAction = <T>() => {
    const operator = pipe<Observable<UiActionMetaInfo<T>>, Observable<UiActionMetaInfo<T>>>(
        tap((uiAction) => uiAction.service.processAction(uiAction)),
    );

    return operator;
};

export const takeCompleted = <T>() => {
    const operator = pipe<Observable<UiActionExecutionInfo<T>>, Observable<UiActionExecutionInfo<T>>>(
        filter((execution) => execution?.status === 'Completed'),
    );

    return operator;
};

// import { Observable, pipe, tap } from 'rxjs';

// import { UiAction } from '../models/ui-action.model';

// export const completeUiCommand = <TO extends UiAction>() => {
//     const operator = pipe<Observable<TO>, Observable<TO>>(tap((uiAction) => uiAction.process()));

//     return operator;
// };

// import { Dictionary } from '@itdl/shared/common';
// import { filter, map, pipe } from 'rxjs';

// import { UiAction } from '../models/ui-action.model';
// import { UiActionWithCommand } from '../models/ui-action-with-command.model';

// export const filterUiActions = <T extends UiAction>(actionTypeInstance: { new (...args: any[]): T; type: string }) => {
//     const operator = pipe(
//         map((actions: Dictionary<string, UiAction[]>) => actions[actionTypeInstance.type] as UiAction[]),
//         filter((actions) => !!actions.length),
//         filter((action) => action instanceof actionTypeInstance),
//         map((item) => item as T),
//     );

//     return operator;
// };

// function inputIsUiActionWithCommand(action: UiAction): action is UiActionWithCommand {
//     return action.currentCommand !== undefined && action.currentCommand !== null;
// }

// function isUiActionWithCommand() {
//     const operator = pipe(filter(inputIsUiActionWithCommand));
//     return operator;
// }

// function inputIsNotNullOrUndefined<T>(input: null | undefined | T): input is T {
//     return input !== null && input !== undefined;
// }

// function isNotNullOrUndefined<T>() {
//     const operator = pipe(filter<T>(inputIsNotNullOrUndefined));
//     return operator;
// }
