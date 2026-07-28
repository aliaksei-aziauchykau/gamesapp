import { GpllUser, IGpllIdToken } from '@itdl/game-portal/features/auth/entities';

export namespace AuthStoreActions {
    export class StoreIsAuthenticated {
        static readonly type = '[Auth] Store Is Authenticated';
        constructor(public isAuthenticated: boolean | null) {}
    }

    export class StoreIdToken {
        static readonly type = '[Auth] Store Id Token';
        constructor(public idToken: IGpllIdToken) {}
    }

    export class StoreUser {
        static readonly type = '[Auth] Store User';
        constructor(public user: GpllUser) {}
    }
}
