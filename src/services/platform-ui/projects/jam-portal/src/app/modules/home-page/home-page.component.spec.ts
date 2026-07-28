import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GplHomeComponent } from './home-page.component';

describe('HomeComponent', () => {
    let component: GplHomeComponent;
    let fixture: ComponentFixture<GplHomeComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [GplHomeComponent],
        }).compileComponents();

        fixture = TestBed.createComponent(GplHomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
