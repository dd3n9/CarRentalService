import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from './core/services/auth.service';
import { Subject, takeUntil } from 'rxjs';
import { ReservationNotificationService } from './core/services/reservation-notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  constructor(
    private authService: AuthService,
    private signalRService: ReservationNotificationService
  ) {}

  ngOnInit(): void {
    this.authService.isAuth$
      .pipe(takeUntil(this.destroy$))
      .subscribe((isAuth) => {
        if (isAuth) {
          this.signalRService.startConnection();
        } else {
          this.signalRService.stopConnection();
        }
      });
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
    this.destroy$.next();
    this.destroy$.complete();
  }
}
