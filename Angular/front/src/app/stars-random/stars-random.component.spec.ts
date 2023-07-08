import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StarsRandomComponent } from './stars-random.component';

describe('StarsRandomComponent', () => {
  let component: StarsRandomComponent;
  let fixture: ComponentFixture<StarsRandomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StarsRandomComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StarsRandomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
