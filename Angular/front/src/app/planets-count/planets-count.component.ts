import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";

@Component({
  selector: 'app-planets-count',
  templateUrl: './planets-count.component.html',
  styleUrls: ['./planets-count.component.css']
})
export class PlanetsCountComponent implements OnInit {
  planetsCount!: number;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllPlanetsCount();
  }

  getAllPlanetsCount(): void {
    this.http.get<number>(host+'/ServiceAPI/planets/count').subscribe(count => {
      this.planetsCount = count;
    });
  }
}