import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import {
  CreateReservationRequest,
  Reservation,
} from '../models/reservations.model';
import { PaginatedResult } from '../models/paginated-result.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationsService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:8085/api/v1/Reservations';

  constructor() {}

  getUserReservations(
    pageSize: number,
    pageNumber: number
  ): Observable<PaginatedResult<Reservation>> {
    let params = new HttpParams()
      .set('pageSize', pageSize.toString())
      .set('pageNumber', pageNumber.toString());

    return this.http
      .get<Reservation[]>(`${this.apiUrl}/my-reservations`, {
        params,
        observe: 'response',
      })
      .pipe(
        map((response) => {
          let pagination: any = null;
          const paginationHeader = response.headers.get('X-Pagination');
          if (paginationHeader) {
            try {
              pagination = JSON.parse(paginationHeader);
            } catch (error) {
              console.error('Error parsing pagination header:', error);
            }
          }

          const paginatedResult: PaginatedResult<Reservation> = {
            items: response.body || [],
            totalPages: pagination?.totalPages || 0,
            totalCount: pagination?.totalItems || 0,
            pageSize: pagination?.pageSize || 0,
            currentPage: pagination?.pageNumber || 0,
          };

          return paginatedResult;
        })
      );
  }
  createReservation(request: CreateReservationRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}`, request);
  }
}
