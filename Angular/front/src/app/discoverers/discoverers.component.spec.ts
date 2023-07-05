import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiscoverersComponent } from './discoverers.component';

describe('DiscoverersComponent', () => {
  let component: DiscoverersComponent;
  let fixture: ComponentFixture<DiscoverersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiscoverersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiscoverersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
