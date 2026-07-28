import { HttpParams } from '@angular/common/http';

export type HttpQueryParams =
    | HttpParams
    | {
          [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>;
      };
