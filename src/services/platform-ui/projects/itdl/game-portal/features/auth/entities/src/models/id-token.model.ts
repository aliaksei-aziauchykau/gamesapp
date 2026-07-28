import { IdToken } from '@auth0/auth0-angular';

import { IGpllIdToken } from '../interfaces/id-token.interfaces';

export abstract class IdTokenBase implements IdToken {
    [key: string]: any;
    __raw!: string;
    name?: string | undefined;
    given_name?: string | undefined;
    family_name?: string | undefined;
    middle_name?: string | undefined;
    nickname?: string | undefined;
    preferred_username?: string | undefined;
    profile?: string | undefined;
    picture?: string | undefined;
    website?: string | undefined;
    email?: string | undefined;
    email_verified?: boolean | undefined;
    gender?: string | undefined;
    birthdate?: string | undefined;
    zoneinfo?: string | undefined;
    locale?: string | undefined;
    phone_number?: string | undefined;
    phone_number_verified?: boolean | undefined;
    address?: string | undefined;
    updated_at?: string | undefined;
    iss?: string | undefined;
    aud?: string | undefined;
    exp?: number | undefined;
    nbf?: number | undefined;
    iat?: number | undefined;
    jti?: string | undefined;
    azp?: string | undefined;
    nonce?: string | undefined;
    auth_time?: string | undefined;
    at_hash?: string | undefined;
    c_hash?: string | undefined;
    acr?: string | undefined;
    amr?: string[] | undefined;
    sub_jwk?: string | undefined;
    cnf?: string | undefined;
    sid?: string | undefined;
    org_id?: string | undefined;
    org_name?: string | undefined;
}

export class GpllIdToken extends IdTokenBase implements IGpllIdToken {
    cus_roles!: string[];
    scopes!: string;

    constructor(idToken: IdToken) {
        super();
        Object.assign(this, idToken);
    }
}
