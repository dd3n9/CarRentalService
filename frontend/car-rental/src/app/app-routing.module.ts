import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarsPageComponent } from './pages/cars-page/cars-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { AppRoutes, UserRoles } from './core/constants/constants';
import { MyReservationsPageComponent } from './pages/my-reservations-page/my-reservations-page.component';
import { canActivateRoles } from './core/guards/access.guard';
import { ManagementPageComponent } from './pages/management-page/management-page.component';

const routes: Routes = [
  { path: AppRoutes.Cars, component: CarsPageComponent },
  { path: AppRoutes.Login, component: LoginPageComponent },
  { path: AppRoutes.Register, component: RegisterPageComponent },
  {
    path: AppRoutes.Reservations,
    component: MyReservationsPageComponent,
    canActivate: [canActivateRoles([])],
  },
  {
    path: AppRoutes.ModeratorDashboard,
    component: ManagementPageComponent,
    canActivate: [canActivateRoles([UserRoles.Moderator])],
  },
  { path: '', redirectTo: `/${AppRoutes.Cars}`, pathMatch: 'full' },
  { path: '**', redirectTo: `/${AppRoutes.Cars}`, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
