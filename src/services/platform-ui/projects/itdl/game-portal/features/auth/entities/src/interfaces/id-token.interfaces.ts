import { IdToken } from '@auth0/auth0-angular';

export interface IGpllIdToken extends IdToken {
    cus_roles: string[];
    scopes: string;
}
