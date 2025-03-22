import { Component, inject } from '@angular/core';
import { AppRoutes } from '../../core/constants/constants';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { GlobalErrorHandlerService } from '../../core/services/global-error-handler.service';

@Component({
  selector: 'app-register-page',
  standalone: false,
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.css',
})
export class RegisterPageComponent {
  authService = inject(AuthService);
  router = inject(Router);
  globalErrorHandlerService = inject(GlobalErrorHandlerService);
  registerData = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
  };
  loading: boolean = false;
  appRoutes = AppRoutes;

  registerUser(): void {
    this.loading = true;
    this.authService.register(this.registerData).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/login']);
        this.globalErrorHandlerService.showSuccess(
          'Registration is successful!'
        );
      },
      error: () => {
        this.loading = false;
      },
    });
  }
}
