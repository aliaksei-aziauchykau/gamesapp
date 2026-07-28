import { HttpHeaders } from '@angular/common/http';

import { HttpQueryParams } from './http-query-params.type';

export type HttpOptions = {
    headers?:
        | HttpHeaders
        | {
              [header: string]: string | string[];
          };
    params?: HttpQueryParams;
    withCredentials?: boolean;
    responseType?: any;
};
