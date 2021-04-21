import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  constructor(private _httpService: HttpClient) { }
  accessPointUrl: string = 'https://localhost:44311/api/ngLocationMaps';
  apiValues: string[] = [];
  ngOnInit() {    
  }
}
