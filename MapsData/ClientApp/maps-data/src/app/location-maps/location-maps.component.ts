import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-location-maps',
  templateUrl: './location-maps.component.html',
  styleUrls: ['./location-maps.component.css']
})
export class LocationMapsComponent implements OnInit {
  maps$: Object;

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.getAllMaps();
  }

  getAllMaps(){
    this.dataService.getMaps().subscribe(
      data => this.maps$ = data);
  }
}
