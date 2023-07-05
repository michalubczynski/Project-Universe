
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";

@Component({
  selector: 'app-hire-discoverer',
  templateUrl: './hire-discoverer.component.html',
  styleUrls: ['./hire-discoverer.component.css']
})
export class HireDiscovererComponent {
  name!: string;
  surname!: string;
  age!: string;

  constructor(private http: HttpClient) { }

  hireNewDiscoverer(): void {

    const parsedAge = parseInt(this.age, 10); 


    const body = {
      name: this.name,
      surname: this.surname,
      age: parsedAge
    };


    this.http.post(host+'/ServiceAPI/discoverer/hire', body).subscribe((response: any) => {

      const hiredPerson = response; // Dane zatrudnionej osoby
      // Wyświetl komunikat potwierdzający z danymi zatrudnionej osoby
        const message = 'Pomyślnie zatrudniono: ' + hiredPerson.name + ' ' + hiredPerson.surname;
        alert(message); 
    });

  }
}


