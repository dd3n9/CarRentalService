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
  currentFilters: any = {};

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {
    this.vehicleService
      .getAvailableVehicles(
        this.currentFilters.city,
        this.currentFilters.startDate,
        this.currentFilters.endDate,
        this.currentFilters.vehicleType,
        this.currentFilters.yearFrom,
        this.currentFilters.yearTo,
        this.currentFilters.searchTerm,
        this.currentFilters.sortByPrice,
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

  onFiltersChange(filters: any): void {
    this.currentFilters = filters;
    this.currentPage = 1;
    this.loadVehicles();
  }

  trackByVehicleId(index: number, vehicle: Vehicle): string | undefined {
    return vehicle.vehicleId;
  }
}
