import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const host = 'http://localhost:5080';

@Component({
  selector: 'app-hire-discoverer',
  templateUrl: './hire-discoverer.component.html',
  styleUrls: ['./hire-discoverer.component.css']
})
export class HireDiscovererComponent {
  newDiscoverer = {
    name: '',
    surname: '',
    age: 0
  };

  constructor(private http: HttpClient) { }

  hireNewDiscoverer() {
    const url = `${host}/ServiceAPI/discoverer/hire?name=${encodeURIComponent(this.newDiscoverer.name)}&surname=${encodeURIComponent(this.newDiscoverer.surname)}&age=${this.newDiscoverer.age}`;

    this.http.post(url, {}).subscribe(
      (response: any) => {
        const hiredPerson = response;
        const message = 'Pomyślnie zatrudniono: ' + hiredPerson.name + ' ' + hiredPerson.surname;
        alert(message);

        // Reset form fields
        this.newDiscoverer.name = '';
        this.newDiscoverer.surname = '';
        this.newDiscoverer.age = 0;
      },
      (error: any) => {
        console.error('An error occurred while hiring a new discoverer:', error);
      }
    );
  }
}
