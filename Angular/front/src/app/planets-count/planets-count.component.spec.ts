import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanetsCountComponent } from './planets-count.component';

describe('PlanetsCountComponent', () => {
  let component: PlanetsCountComponent;
  let fixture: ComponentFixture<PlanetsCountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanetsCountComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlanetsCountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
