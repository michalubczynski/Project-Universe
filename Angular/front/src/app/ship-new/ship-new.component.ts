import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

const host = 'http://localhost:5080';

interface Discoverer {
  surname: string;
  age: number;
  ship?: number;
  id: number;
  name: string;
}

@Component({
  selector: 'app-ship-new',
  templateUrl: './ship-new.component.html',
  styleUrls: ['./ship-new.component.css']
})
export class ShipNewComponent implements OnInit {
  maxRange!: number;
  maxSpeed!: number;
  model!: string;
  selectedDiscoverer!: number;
  discoverers!: Discoverer[];

  successMessage: boolean = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.loadDiscoverers();
  }

  loadDiscoverers() {
    const url = `${host}/ServiceAPI/discoverer/Show`;

    this.http.get<Discoverer[]>(url).subscribe((discoverers: Discoverer[]) => {
      this.discoverers = discoverers;
    });
  }

  makeNewShip() {
    const url = `${host}/ServiceAPI/ship/new?MaxRange=${this.maxRange}&MaxSpeed=${this.maxSpeed}&model=${this.model}&discoverer=${this.selectedDiscoverer}`;
    this.http.post(url, {}).subscribe(() => {
      this.successMessage = true;
    });
  }
}
