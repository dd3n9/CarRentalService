import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RentalPointSuggestion } from '../models/rentalPoint.model';

@Injectable({
  providedIn: 'root',
})
export class RentalpointsService {
  private apiUrl = 'https://localhost:8085/api/v1/RentalPoints';
  constructor(private http: HttpClient) {}

  getRentalPointSuggestions(
    prefix: string
  ): Observable<RentalPointSuggestion[]> {
    const params = new HttpParams().set('prefix', prefix);
    return this.http.get<RentalPointSuggestion[]>(
      `${this.apiUrl}/suggestions`,
      { params }
    );
  }

  createRentalPoint(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, data);
  }

  deleteRentalPoint(rentalPointId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}`, {
      params: { rentalPointId },
    });
  }
}
