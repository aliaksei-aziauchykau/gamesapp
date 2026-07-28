import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GamePortalComponent } from './game-portal.component';

describe('GamePortalComponent', () => {
  let component: GamePortalComponent;
  let fixture: ComponentFixture<GamePortalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GamePortalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GamePortalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
