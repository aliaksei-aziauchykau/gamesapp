export namespace UserActions {
    export class GenerateUserId {
        static readonly type = '[User] Generate User Id';
    }

    export class StoreUserId {
        static readonly type = '[User] Store User Id';
        constructor(public userId: string) {}
    }
}
