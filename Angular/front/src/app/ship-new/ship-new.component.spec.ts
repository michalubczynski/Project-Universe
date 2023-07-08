import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipNewComponent } from './ship-new.component';

describe('ShipNewComponent', () => {
  let component: ShipNewComponent;
  let fixture: ComponentFixture<ShipNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShipNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShipNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
