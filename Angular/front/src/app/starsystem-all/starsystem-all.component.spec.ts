import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StarsystemAllComponent } from './starsystem-all.component';

describe('StarsystemAllComponent', () => {
  let component: StarsystemAllComponent;
  let fixture: ComponentFixture<StarsystemAllComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StarsystemAllComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StarsystemAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
