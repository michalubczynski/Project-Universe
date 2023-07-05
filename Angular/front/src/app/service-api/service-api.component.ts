import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";

@Component({
  selector: 'app-service-api',
  templateUrl: './service-api.component.html',
  styleUrls: ['./service-api.component.css']
})

export class ServiceApiComponent implements OnInit {
  planetsCount!: number;
  heaviestPlanet: any;
  discoverers: any[] = [];

  constructor(private http: HttpClient) { }


  ngOnInit(): void {
  }

}
