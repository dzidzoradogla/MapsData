import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { DataService } from '../data.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { MatSort } from '@angular/material/sort';



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
  @ViewChild(MatSort) sort: MatSort;


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  
  ngOnInit(): void {
    this.getAllData();
    this.dataSource = new MatTableDataSource(this.data$);
  }

  applyFilter(event: Event) {
    //const filterValue = (event.target as HTMLInputElement).value;
    //this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getAllData(){
    this.dataService.getData().subscribe(
      data => {
        this.data$ = data;
      this.dataSource.data = data});
  }

}
