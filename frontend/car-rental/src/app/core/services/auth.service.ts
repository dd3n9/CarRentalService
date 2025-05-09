import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthenticationResult } from '../models/auth.model';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { baseUrlV1, UserRoles } from '../constants/constants';
import { GlobalErrorHandlerService } from './global-error-handler.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  http = inject(HttpClient);
  router = inject(Router);
  cookieService = inject(CookieService);
  private apiUrl = `${baseUrlV1}/Authentication`;
  errorHanlder = inject(GlobalErrorHandlerService);

  token: string | null = null;
  private rolesSubject = new BehaviorSubject<string[]>(
    this.getRolesFromCookies()
  );
  public roles$: Observable<string[]> = this.rolesSubject.asObservable();

  private isAuthSubject = new BehaviorSubject<boolean>(
    this.checkInitialAuthState()
  );
  public isAuth$ = this.isAuthSubject.asObservable();

  private checkInitialAuthState(): boolean {
    const token = this.cookieService.get('token');
    this.token = token;
    return !!token;
  }

  get isAuth(): boolean {
    return this.isAuthSubject.value;
  }

  get currentRoles(): string[] {
    return this.rolesSubject.value;
  }

  hasRole(role: UserRoles): boolean {
    return this.currentRoles.includes(role);
  }

  login(payload: { email: string; password: string }) {
    console.log(payload);
    return this.http
      .post<AuthenticationResult>(`${this.apiUrl}/login`, payload, {
        withCredentials: true,
      })
      .pipe(
        tap((val) => {
          this.saveToken(val.token);
          this.saveRoles(val.authenticationDto.userRoles);
          this.isAuthSubject.next(true);
        }),
        catchError((err) => {
          this.errorHanlder.showError(err);
          return throwError(err);
        })
      );
  }

  register(payload: {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
  }) {
    console.log(payload);
    return this.http
      .post<AuthenticationResult>(`${this.apiUrl}/register`, payload, {})
      .pipe(
        catchError((err) => {
          this.errorHanlder.showError(err);
          return throwError(err);
        })
      );
  }

  refreshAuthToken() {
    return this.http
      .post<string>(`${this.apiUrl}/refreshToken`, null, {
        headers: new HttpHeaders({
          Authorization: `Bearer ${this.token}`,
        }),
        withCredentials: true,
      })
      .pipe(
        tap((val) => {
          console.log(val);
          this.saveToken(val);
          this.isAuthSubject.next(true);
        }),
        catchError((err) => {
          console.log(err);
          this.errorHanlder.showError(err);
          return throwError(err);
        })
      );
  }

  logout() {
    this.cookieService.deleteAll();
    this.token = null;
    this.rolesSubject.next([]);
    this.isAuthSubject.next(false);
    this.router.navigate(['/login']);
  }

  private saveToken(token: string) {
    this.token = token;
    this.cookieService.set('token', this.token);
  }

  private saveRoles(roles: string[]) {
    this.rolesSubject.next(roles);
    this.cookieService.set('roles', JSON.stringify(roles));
  }

  private getRolesFromCookies(): string[] {
    const rolesCookie = this.cookieService.get('roles');
    if (rolesCookie) {
      try {
        return JSON.parse(rolesCookie) as string[];
      } catch (e) {
        console.error('Error parsing roles from cookie', e);
        return [];
      }
    }
    return [];
  }
}
