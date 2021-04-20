import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LocationDataComponent } from './location-data/location-data.component';
import { LocationMapsComponent } from './location-maps/location-maps.component';

const routes: Routes = [
  // { 
  //   path: '',   
  //   redirectTo: '/data',
  // },{
  //   path: 'data',
  //   component: LocationDataComponent
  // },
  // {
  //   path: 'maps',
  //   component: LocationMapsComponent
  // }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
