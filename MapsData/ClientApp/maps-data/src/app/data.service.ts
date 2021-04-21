import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

const BASE_URL = 'https://localhost:44311/api/';

@Injectable({
  providedIn: 'root'
})

export class DataService {
  constructor(private http: HttpClient, private router: Router) { }

  getMaps() {
    const url = BASE_URL + 'ngLocationMaps';
    return this.http.get(url) ;
  }

  getData(){
    const url = BASE_URL + 'ngLocationData';
    return this.http.get(url) ;
  }
}
