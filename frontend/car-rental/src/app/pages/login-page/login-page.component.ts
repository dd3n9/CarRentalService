import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { AppRoutes } from '../../core/constants/constants';

@Component({
  selector: 'app-login-page',
  standalone: false,
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css',
})
export class LoginPageComponent {
  authService = inject(AuthService);
  router = inject(Router);
  loginData = {
    email: '',
    password: '',
  };
  loading: boolean = false;

  appRoutes = AppRoutes;

  loginUser(): void {
    this.loading = true;
    this.authService.login(this.loginData).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/cars']);
      },
      error: () => {
        this.loading = false;
      },
    });
  }
}
