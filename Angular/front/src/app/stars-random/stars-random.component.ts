import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";

@Component({
  selector: 'app-stars-random',
  templateUrl: './stars-random.component.html',
  styleUrls: ['./stars-random.component.css']
})

export class StarsRandomComponent implements OnInit {

  count: number = 0;
  countOptions: number[] = [1, 2 ,5 ,10, 20, 30];
  
  constructor(private http: HttpClient) { }

  ngOnInit(): void {}

  addRandomStars(): void {
    if (this.count > 0) {

      const countParam = this.count.toString(); // Konwertuj liczbę na ciąg znaków

      this.http.post(`${host}/ServiceAPI/stars/random?count=${countParam}`,{}).subscribe(
        () => {
          alert(`Dodano ${this.count} losowych gwiazd.`);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
 
}



