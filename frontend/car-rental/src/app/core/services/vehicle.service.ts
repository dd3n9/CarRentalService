import { Injectable } from '@angular/core';
import { Vehicle, VehicleDetailsResponse } from '../models/car.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../models/paginated-result.model';
import { map, Observable } from 'rxjs';
import { baseUrlV1 } from '../constants/constants';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private apiUrl = `${baseUrlV1}/Vehicles`;

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
    const processedStartDate = this.toValidDate(startDate);
    if (processedStartDate) {
      params = params.set('startDate', processedStartDate.toISOString());
    }

    const processedEndDate = this.toValidDate(endDate);
    if (processedEndDate) {
      params = params.set('endDate', processedEndDate.toISOString());
    }
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
  getVehiclesReportPdf(params: any): Observable<Blob> {
    let httpParams = new HttpParams();
    if (params.city) httpParams = httpParams.set('city', params.city);
    if (params.yearFrom)
      httpParams = httpParams.set('yearFrom', params.yearFrom);
    if (params.yearTo) httpParams = httpParams.set('yearTo', params.yearTo);
    if (params.seats) httpParams = httpParams.set('seats', params.seats);
    if (params.type) httpParams = httpParams.set('type', params.type);

    return this.http.get(`${this.apiUrl}/report-pdf`, {
      params: httpParams,
      responseType: 'blob',
    });
  }

  getVehicleDetails(vehicleId: string): Observable<VehicleDetailsResponse> {
    return this.http.get<VehicleDetailsResponse>(
      `${this.apiUrl}/${vehicleId}/details`
    );
  }

  createVehicle(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, data);
  }

  deleteVehicle(vehicleId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${vehicleId}`);
  }

  returnVehicle(reservationId: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${reservationId}/return`, {});
  }

  private toValidDate(value?: Date | string): Date | null {
    if (!value) return null;

    if (value instanceof Date && !isNaN(value.getTime())) {
      return value;
    }

    if (typeof value === 'string') {
      const date = new Date(value);
      return !isNaN(date.getTime()) ? date : null;
    }

    return null;
  }
}
