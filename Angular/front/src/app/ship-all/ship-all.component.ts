import { Component } from '@angular/core';
import { Ship } from '../Models/Ship.model';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";

@Component({
  selector: 'app-ship-all',
  templateUrl: './ship-all.component.html',
  styleUrls: ['./ship-all.component.css']
})
export class ShipAllComponent {
  ships!: Ship[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getShips();
  }

  getShips() {
    this.http.get<Ship[]>(host+'/ServiceAPI/ship/all').subscribe(
      (data) => {
        this.ships = data;
      },
      (error) => {
        console.log('An error occurred while fetching ships:', error);
      }
    );
  }
}
