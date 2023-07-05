import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";

@Component({
  selector: 'app-heaviest-planet',
  templateUrl: './heaviest-planet.component.html',
  styleUrls: ['./heaviest-planet.component.css']
})
export class HeaviestPlanetComponent implements OnInit {
  heaviestPlanet: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getHeaviestPlanet();
  }

  getHeaviestPlanet(): void {
    this.http.get<any>(host+'/ServiceAPI/planets/heaviest').subscribe(planet => {
      this.heaviestPlanet = planet;
    });
  }


}