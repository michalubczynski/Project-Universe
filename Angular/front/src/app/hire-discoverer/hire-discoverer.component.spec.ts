import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HireDiscovererComponent } from './hire-discoverer.component';

describe('HireDiscovererComponent', () => {
  let component: HireDiscovererComponent;
  let fixture: ComponentFixture<HireDiscovererComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HireDiscovererComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HireDiscovererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
