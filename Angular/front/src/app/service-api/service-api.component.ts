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
    this.getAllPlanetsCount();
    this.getHeaviestPlanet();
    this.getDiscoverers();
  }

  getAllPlanetsCount(): void {
    this.http.get<number>(host+'/ServiceAPI/planets/count').subscribe(count => {
      this.planetsCount = count;
    });
  }

  getHeaviestPlanet(): void {
    this.http.get<any>(host+'/ServiceAPI/planets/heaviest').subscribe(planet => {
      this.heaviestPlanet = planet;
    });
  }

  getDiscoverers(): void {
    this.http.get<any[]>(host+'/ServiceAPI/discoverer/Show').subscribe(discoverers => {
      this.discoverers = discoverers;
    });
  }

  hireNewDiscoverer(name: string, surname: string, age: string): void {

    const parsedAge = parseInt(age, 10); 

    const body = {
      name: name,
      surname: surname,
      age: parsedAge
    };

    this.http.post('/ServiceAPI/discoverer/hire', body).subscribe((response: any) => {
      const hiredPerson = response; // Dane zatrudnionej osoby
  
      // Wyświetl komunikat potwierdzający z danymi zatrudnionej osoby
      const message = 'Pomyślnie zatrudniono: ' + hiredPerson.name + ' ' + hiredPerson.surname;
      alert(message); 

    });
  }
}
