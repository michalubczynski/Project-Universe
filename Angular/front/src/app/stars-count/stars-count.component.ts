import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var host = "http://localhost:5080";


@Component({
  selector: 'app-stars-count',
  templateUrl: './stars-count.component.html',
  styleUrls: ['./stars-count.component.css']
})
export class StarsCountComponent {
  starsCount: number | null = null;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getStarsCount();
  }

  getStarsCount() {
    this.http.get<number>(`${host}/ServiceAPI/stars/count`).subscribe(count => {
      this.starsCount = count;
  });

  }
}

