import { Subject } from 'rxjs';

export abstract class Destroyable {
    protected onDestroy = new Subject<void>();

    protected _ngOnDestroy() {
        this.onDestroy.next();
        this.onDestroy.complete();
    }
}
