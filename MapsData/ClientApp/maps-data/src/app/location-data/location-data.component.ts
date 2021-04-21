import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { DataService } from '../data.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';



@Component({
  selector: 'app-location-data',
  templateUrl: './location-data.component.html',
  styleUrls: ['./location-data.component.css']
})
export class LocationDataComponent implements OnInit, AfterViewInit {
  data$: any;
  dataSource: any;
  displayedColumns: string[] = ['Location', 'Time', 'Atmospheric Pressure', 'Gust', 'Wind Speed', 'Wind Direction'];

  constructor(private dataService: DataService) { }

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  
  ngOnInit(): void {
    this.getAllData();
    this.dataSource = new MatTableDataSource(this.data$);
    console.log(this.data$);
  }

  getAllData(){
    this.dataService.getData().subscribe(
      data => {
        this.data$ = data});
  }

}
