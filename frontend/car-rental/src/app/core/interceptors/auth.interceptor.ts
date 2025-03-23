import {
  HttpErrorResponse,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { GlobalErrorHandlerService } from '../services/global-error-handler.service';

let isRefreshing = false;

export const authTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const errorHandlerService = inject(GlobalErrorHandlerService);
  const token = authService.token;

  if (!token || req.url.includes('/refreshToken')) {
    return next(req);
  }

  const authReq = addToken(req, token);

  return next(authReq).pipe(
    catchError((error) => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        if (!isRefreshing) {
          isRefreshing = true;
          return authService.refreshAuthToken().pipe(
            switchMap((newToken) => {
              isRefreshing = false;
              return next(addToken(req, newToken));
            }),
            catchError((refreshError) => {
              isRefreshing = false;
              authService.logout();
              return throwError(() => refreshError);
            })
          );
        } else {
          return next(addToken(req, authService.token!));
        }
      }
      errorHandlerService.showError(error);
      return throwError(() => error);
    })
  );
};

const addToken = (req: HttpRequest<any>, token: string) => {
  return (req = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
    },
  }));
};
