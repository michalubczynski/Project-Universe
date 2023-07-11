import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FireDiscovererComponent } from './fire-discoverer.component';

describe('FireDiscovererComponent', () => {
  let component: FireDiscovererComponent;
  let fixture: ComponentFixture<FireDiscovererComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FireDiscovererComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FireDiscovererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
