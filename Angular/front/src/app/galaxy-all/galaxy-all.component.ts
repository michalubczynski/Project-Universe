import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Galaxy } from '../Models/Galaxy.model'
var host = "http://localhost:5080";

@Component({
  selector: 'app-galaxy',
  templateUrl: './galaxy-all.component.html',
  styleUrls: ['./galaxy-all.component.css']
})
export class GalaxyAllComponent implements OnInit {
  galaxies: Galaxy[] = [];
  

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllGalaxies();
    for (const galaxy of this.galaxies){
      console.log(galaxy.mass);
    }
  }

  getAllGalaxies(): void {
    this.http.get<Galaxy[]>(host+'/ServiceAPI/Galaxy/all').subscribe(
      (galaxies: Galaxy[]) => {
        this.galaxies = galaxies;
      },
      (error) => {
        console.error('Error occurred while retrieving galaxies:', error);
      }
    );


  }
}
