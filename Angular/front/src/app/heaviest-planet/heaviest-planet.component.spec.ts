import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaviestPlanetComponent } from './heaviest-planet.component';

describe('HaviestPlanetComponent', () => {
  let component: HeaviestPlanetComponent;
  let fixture: ComponentFixture<HeaviestPlanetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HeaviestPlanetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HeaviestPlanetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
