import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const host = 'http://localhost:5080';

@Component({
  selector: 'app-fire-discoverer',
  templateUrl: './fire-discoverer.component.html',
  styleUrls: ['./fire-discoverer.component.css']
})
export class FireDiscovererComponent {
  discovererId: number | undefined;

  constructor(private http: HttpClient) { }

  fireDiscoverer() {
    const url = `${host}/ServiceAPI/discoverer/fire?id=${this.discovererId}`;

    this.http.delete(url).subscribe(
      () => {
        alert('Discoverer fired successfully');
        this.discovererId = undefined; // Reset the discoverer ID field
      },
      (error: any) => {
        console.error('An error occurred while firing the discoverer:', error);
      }
    );
  }
}
