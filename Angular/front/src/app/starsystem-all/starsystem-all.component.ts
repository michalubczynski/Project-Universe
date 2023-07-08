import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StarSystem } from '../Models/star-system.model';
var host = "http://localhost:5080";

@Component({
  selector: 'app-starsystem',
  templateUrl: './starsystem-all.component.html',
  styleUrls: ['./starsystem-all.component.css']
})
export class StarsystemAllComponent implements OnInit {
  starSystems: StarSystem[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllStarSystems();
  }

  getAllStarSystems(): void {
    this.http.get<StarSystem[]>(host+'/ServiceAPI/starsystem/all').subscribe(
      (starSystems: StarSystem[]) => {
        this.starSystems = starSystems;
      },
      (error) => {
        console.error('Error occurred while retrieving star systems:', error);
      }
    );
  }
}
