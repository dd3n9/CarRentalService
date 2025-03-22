import { Component, OnInit } from '@angular/core';
import { Vehicle, VehicleDetailsResponse } from '../../core/models/car.model';
import { PaginatedResult } from '../../core/models/paginated-result.model';
import { VehicleService } from '../../core/services/vehicle.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RentalPointSuggestion } from '../../core/models/rentalPoint.model';
import { RentalpointsService } from '../../core/services/rentalpoints.service';
import { CreateReservationRequest } from '../../core/models/reservations.model';
import { ReservationsService } from '../../core/services/reservations.service';

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

  showDetailsModal: boolean = false;
  vehicleDetails: VehicleDetailsResponse | null = null;
  detailsModalError: string | null = null;

  showReserveModal: boolean = false;
  selectedVehicleId: string | null = null;
  reserveModalError: string | null = null;

  constructor(
    private vehicleService: VehicleService,
    private reservationService: ReservationsService
  ) {}

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

  onViewDetails(vehicleId: string): void {
    this.vehicleService.getVehicleDetails(vehicleId).subscribe({
      next: (details: VehicleDetailsResponse) => {
        this.vehicleDetails = details;
        this.showDetailsModal = true;
        this.detailsModalError = null;
      },
      error: (errorResponse: HttpErrorResponse) => {
        console.error('Error fetching vehicle details:', errorResponse);
        this.detailsModalError =
          'Failed to load vehicle details. Please try again later.';
        this.vehicleDetails = null;
        this.showDetailsModal = true;
      },
    });
  }

  closeDetailsModal(): void {
    this.showDetailsModal = false;
    this.vehicleDetails = null;
    this.detailsModalError = null;
  }

  onReserve(vehicleId: string): void {
    this.selectedVehicleId = vehicleId;
    this.showReserveModal = true;
    this.reserveModalError = null;
  }

  closeReserveModal(): void {
    this.showReserveModal = false;
    this.selectedVehicleId = null;
    this.reserveModalError = null;
  }

  submitReservation(request: CreateReservationRequest): void {
    this.reservationService.createReservation(request).subscribe({
      next: (response) => {
        alert('Reservation created successfully!');
        this.closeReserveModal();
        this.loadVehicles();
      },
      error: (errorResponse: HttpErrorResponse) => {
        console.error('Error creating reservation:', errorResponse);
        this.reserveModalError =
          'Failed to create reservation. Please try again.';
      },
    });
  }
}
