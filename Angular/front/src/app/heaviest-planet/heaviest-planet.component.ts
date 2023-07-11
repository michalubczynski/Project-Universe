import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Planet } from '../Models/Planet.model';
import { StarSystem } from '../Models/star-system.model';
var host = "http://localhost:5080";

@Component({
  selector: 'app-heaviest-planet',
  templateUrl: './heaviest-planet.component.html',
  styleUrls: ['./heaviest-planet.component.css']
})
export class HeaviestPlanetComponent implements OnInit {
  heaviestPlanet: Planet | undefined;
  starSystems: StarSystem[] = [];
  currentStarSystem: StarSystem | undefined;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getHeaviestPlanet();
    this.getAllStarSystems();
  }

  getHeaviestPlanet(): void {
    this.http.get<any>(host+'/ServiceAPI/planets/heaviest').subscribe(planet => {
      this.heaviestPlanet = planet;
    });
  }
  getAllStarSystems(): void {
    this.http.get<any>(host + '/ServiceAPI/starsystem/all').subscribe(_starSystem => {
      this.starSystems = _starSystem;
      console.log('Retrieved star systems:', this.starSystems);
      this.compareStarSystem();
    });
  }
  compareStarSystem(): void {  
    for (const starSystem of this.starSystems) {
      if (this.heaviestPlanet?.starSystemId == starSystem.id) {
        this.currentStarSystem = starSystem;
      }
    }
  }

}