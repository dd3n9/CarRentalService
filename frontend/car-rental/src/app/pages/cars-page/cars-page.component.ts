import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../../core/models/car.model';
import { PaginatedResult } from '../../core/models/paginated-result.model';
import { VehicleService } from '../../core/services/vehicle.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-cars-page',
  standalone: false,
  templateUrl: './cars-page.component.html',
  styleUrl: './cars-page.component.css',
})
export class CarsPageComponent implements OnInit {
  vehicles: Vehicle[] = [];
  paginationInfo: PaginatedResult<Vehicle> | null = null;
  error: string | null = null;
  currentPage: number = 1;
  pageSize: number = 10;

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {
    this.vehicleService
      .getAvailableVehicles(
        undefined, // city
        undefined, // startDate
        undefined, // endDate
        undefined, // vehicleType
        undefined, // yearFrom
        undefined, // yearTo
        undefined, // searchTerm
        undefined, // sortByPrice
        this.pageSize,
        this.currentPage
      )
      .subscribe({
        next: (paginatedResult: PaginatedResult<Vehicle>) => {
          this.vehicles = paginatedResult.items;
          this.paginationInfo = paginatedResult;
          this.error = null;
          console.log('Pagination Info:', this.paginationInfo);
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.error('Error fetching vehicles:', errorResponse);
          this.vehicles = [];
          this.paginationInfo = null;
          this.error = 'Failed to load vehicles. Please try again later.';
        },
      });
  }

  onPageChange(pageNumber: number): void {
    this.currentPage = pageNumber;
    this.loadVehicles();
  }

  trackByVehicleId(index: number, vehicle: Vehicle): string | undefined {
    return vehicle.vehicleId;
  }

  getPagesArray(): number[] {
    if (!this.paginationInfo) {
      return [];
    }
    const pagesCount = this.paginationInfo.totalPages;
    return Array.from({ length: pagesCount }, (_, index) => index + 1); // Create array [1, 2, 3, ..., totalPages]
  }
}
