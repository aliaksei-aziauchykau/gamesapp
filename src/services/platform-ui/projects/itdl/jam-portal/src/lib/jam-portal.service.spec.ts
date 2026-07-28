import { TestBed } from '@angular/core/testing';

import { GamePortalService } from './jam-portal.service';

describe('GamePortalService', () => {
    let service: GamePortalService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(GamePortalService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
