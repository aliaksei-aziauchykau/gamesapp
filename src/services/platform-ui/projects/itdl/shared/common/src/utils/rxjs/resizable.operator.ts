import { Observable } from 'rxjs';

export class ResizableResult<T extends HTMLElement> {
    public element!: T;
    public entries!: ResizeObserverEntry[];

    constructor(data: Omit<ResizableResult<T>, 'entry' | 'widthHeight'>) {
        Object.assign(this, data);
    }

    public get widthHeight() {
        return { width: this.entry.contentRect.width, height: this.entry.contentRect.height };
    }

    public get entry() {
        return this.entries[0];
    }
}

export const resizable = <T extends HTMLElement>(element: T) => {
    return new Observable<ResizableResult<T>>((subscriber) => {
        const resizeObserver = new ResizeObserver((entries) => {
            subscriber.next(new ResizableResult({ element, entries }));
        });

        resizeObserver.observe(element);

        return () => resizeObserver.disconnect();
    });
};
