import { Injectable } from '@angular/core';
import { Vehicle } from '../models/car.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../models/paginated-result.model';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private apiUrl = 'https://localhost:8085/api/v1/Vehicles';

  constructor(private http: HttpClient) {}

  getAvailableVehicles(
    city?: string,
    startDate?: Date,
    endDate?: Date,
    vehicleType?: string,
    yearFrom?: number,
    yearTo?: number,
    searchTerm?: string,
    sortByPrice?: string,
    pageSize: number = 10,
    pageNumber: number = 1
  ): Observable<PaginatedResult<Vehicle>> {
    let params = new HttpParams()
      .set('pageSize', pageSize.toString())
      .set('pageNumber', pageNumber.toString());

    if (city) params = params.set('city', city);
    if (startDate) params = params.set('startDate', startDate.toISOString());
    if (endDate) params = params.set('endDate', endDate.toISOString());
    if (vehicleType) params = params.set('vehicleType', vehicleType);
    if (yearFrom) params = params.set('yearFrom', yearFrom.toString());
    if (yearTo) params = params.set('yearTo', yearTo.toString());
    if (searchTerm) params = params.set('searchTerm', searchTerm);
    if (sortByPrice) params = params.set('sortByPrice', sortByPrice);

    return this.http
      .get<Vehicle[]>(`${this.apiUrl}/available`, {
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

          const paginatedResult: PaginatedResult<Vehicle> = {
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
}
