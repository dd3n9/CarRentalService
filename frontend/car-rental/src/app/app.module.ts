import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CarsPageComponent } from './pages/cars-page/cars-page.component';
import { HttpClientModule } from '@angular/common/http';
import { VehicleService } from './core/services/vehicle.service';
import { CarCardComponent } from './components/car-card/car-card.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { CarFilterComponent } from './components/car-filter/car-filter.component';

@NgModule({
  declarations: [AppComponent, CarsPageComponent, CarCardComponent, PaginationComponent, CarFilterComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [VehicleService],
  bootstrap: [AppComponent],
})
export class AppModule {}
