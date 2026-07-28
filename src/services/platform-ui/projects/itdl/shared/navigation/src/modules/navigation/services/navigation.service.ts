import { Injectable } from '@angular/core';
import { from } from 'rxjs';

import { scrollIntoViewPromise } from '../utils/scroll-into-view.promise';

@Injectable()
export class NavigationService {
    public scrollToElement$(domElement: Element, options: ScrollIntoViewOptions = { behavior: 'smooth' }) {
        return from(scrollIntoViewPromise(domElement, options));
    }
}
