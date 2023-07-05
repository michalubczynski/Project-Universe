import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";

@Component({
  selector: 'app-discoverers',
  templateUrl: './discoverers.component.html',
  styleUrls: ['./discoverers.component.css']
})
export class DiscoverersComponent implements OnInit {
  discoverers: any[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getDiscoverers();
  }

  getDiscoverers(): void {
    this.http.get<any[]>(host+'/ServiceAPI/discoverer/Show').subscribe(discoverers => {
      this.discoverers = discoverers;
    });
  }

}
