import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CarsPageComponent } from './pages/cars-page/cars-page.component';
import {
  HTTP_INTERCEPTORS,
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { VehicleService } from './core/services/vehicle.service';
import { CarCardComponent } from './components/car-card/car-card.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { CarFilterComponent } from './components/car-filter/car-filter.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MyReservationsPageComponent } from './pages/my-reservations-page/my-reservations-page.component';
import { authTokenInterceptor } from './core/interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    CarsPageComponent,
    CarCardComponent,
    PaginationComponent,
    CarFilterComponent,
    NavbarComponent,
    LoginPageComponent,
    RegisterPageComponent,
    MyReservationsPageComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    VehicleService,
    provideHttpClient(withInterceptors([authTokenInterceptor])),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
