import { Component, inject } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { map, Observable } from 'rxjs';
import { AppRoutes, UserRoles } from '../../core/constants/constants';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  isModerator$: Observable<boolean>;
  appRoutes = AppRoutes;

  constructor(public authService: AuthService) {
    this.isModerator$ = this.authService.roles$.pipe(
      map((roles) => roles.includes(`${UserRoles.Moderator}`))
    );
  }

  logout(): void {
    this.authService.logout();
  }
}
