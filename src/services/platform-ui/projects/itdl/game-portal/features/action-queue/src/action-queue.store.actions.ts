export namespace ActionQueueStoreActions {
    export class AddToQueue {
        static readonly type = '[ActionQueue] Add To Queue';
        constructor(public action: unknown) {}
    }
}
