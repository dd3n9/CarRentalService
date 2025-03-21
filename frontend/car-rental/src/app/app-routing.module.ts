import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarsPageComponent } from './pages/cars-page/cars-page.component';

const routes: Routes = [
  { path: 'cars', component: CarsPageComponent },
  //{ path: 'reservations', component: UserReservationsComponent, canActivate: [canActivateAuth] },
  //{ path: 'moderator', component: ModeratorDashboardComponent, canActivate: [canActivateRoles(['MANAGER'])] },
  { path: '', redirectTo: '/cars', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
