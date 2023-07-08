import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GalaxyAllComponent } from './galaxy-all.component';

describe('GalaxyAllComponent', () => {
  let component: GalaxyAllComponent;
  let fixture: ComponentFixture<GalaxyAllComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GalaxyAllComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GalaxyAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
