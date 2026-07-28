import { User } from '@auth0/auth0-angular';

export class GpllUser extends User {
    constructor(user: User) {
        super();
        Object.assign(this, user);
    }
}
