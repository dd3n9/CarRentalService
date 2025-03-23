import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from './auth.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { GlobalErrorHandlerService } from './global-error-handler.service';

@Injectable({
  providedIn: 'root',
})
export class ReservationNotificationService {
  private hubConnection: signalR.HubConnection;
  private notificationSubject = new BehaviorSubject<string | null>(null);
  public notifications$: Observable<string | null> =
    this.notificationSubject.asObservable();

  private reconnectAttempts = 0;
  private maxReconnectAttempts = 5;
  private reconnectDelay = 5000;

  constructor(
    private authService: AuthService,
    private notificationHandler: GlobalErrorHandlerService
  ) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:8085/reservations/reservation-hub', {
        accessTokenFactory: () => this.authService.token || '',
      })
      .withAutomaticReconnect()
      .build();
    this.registerOnServerEvents();
  }

  public startConnection(): void {
    if (!this.authService.isAuth) {
      console.warn(
        'Cannot start SignalR connection: User is not authenticated.'
      );
      return;
    }

    if (this.hubConnection.state === signalR.HubConnectionState.Disconnected) {
      this.hubConnection
        .start()
        .then(() => {
          console.log('SignalR Connected!');
          this.reconnectAttempts = 0;
        })
        .catch((err) => {
          console.error('Error while starting SignalR connection:', err);
          this.handleReconnection();
        });
    }
  }

  public stopConnection(): void {
    if (this.hubConnection.state === signalR.HubConnectionState.Connected) {
      this.hubConnection
        .stop()
        .then(() => console.log('SignalR Disconnected!'))
        .catch((err) =>
          console.error('Error while stopping SignalR connection:', err)
        );
    }
  }

  private registerOnServerEvents(): void {
    this.hubConnection.on('ReceiveNotification', (message: string) => {
      this.notificationSubject.next(message);
      this.notificationHandler.showWarning(message, 'Reminder', {
        timeOut: 10000,
      });
    });

    this.hubConnection.onreconnected(() => {
      console.log('SignalR Reconnected!');
    });

    this.hubConnection.onreconnecting((error) => {
      console.log('SignalR Reconnecting...', error);
    });

    this.hubConnection.onclose((error) => {
      console.log('SignalR Connection Closed:', error);
    });
  }

  private handleReconnection(): void {
    if (this.reconnectAttempts < this.maxReconnectAttempts) {
      this.reconnectAttempts++;
      console.log(
        `Attempting to reconnect (${this.reconnectAttempts}/${this.maxReconnectAttempts})...`
      );
      setTimeout(() => this.startConnection(), this.reconnectDelay);
    } else {
      console.error(
        'Max reconnect attempts reached. SignalR connection failed.'
      );
      this.notificationHandler.showError(
        'Failed to connect to notifications server. Please refresh the page.'
      );
    }
  }
}
