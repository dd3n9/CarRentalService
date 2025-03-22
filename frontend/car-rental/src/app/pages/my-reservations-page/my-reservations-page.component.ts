import { Component, inject } from '@angular/core';
import { ReservationsService } from '../../core/services/reservations.service';
import { GlobalErrorHandlerService } from '../../core/services/global-error-handler.service';
import { Reservation } from '../../core/models/reservations.model';
import { PaginatedResult } from '../../core/models/paginated-result.model';
import { HttpErrorResponse } from '@angular/common/http';
import { VehicleService } from '../../core/services/vehicle.service';

@Component({
  selector: 'app-my-reservations-page',
  standalone: false,
  templateUrl: './my-reservations-page.component.html',
  styleUrl: './my-reservations-page.component.css',
})
export class MyReservationsPageComponent {
  vehicles: Reservation[] = [];
  paginationInfo: PaginatedResult<Reservation> | null = null;
  error: string | null = null;
  currentPage: number = 1;
  pageSize: number = 10;
  currentFilters: any = {};
  loading: boolean = false;

  constructor(
    private reservationsService: ReservationsService,
    private vehicleService: VehicleService
  ) {}

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {
    this.loading = true;
    this.reservationsService
      .getUserReservations(this.pageSize, this.currentPage)
      .subscribe({
        next: (paginatedResult: PaginatedResult<Reservation>) => {
          this.vehicles = paginatedResult.items;
          this.paginationInfo = paginatedResult;
          this.error = null;
          this.loading = false;
        },
        error: (errorResponse: HttpErrorResponse) => {
          this.vehicles = [];
          this.paginationInfo = null;
          this.loading = false;
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

  trackByVehicleId(
    index: number,
    reservation: Reservation
  ): string | undefined {
    return reservation.reservationId;
  }
  deleteReservation(reservationId: string): void {
    if (confirm('Are you sure you want to delete this reservation?')) {
      this.reservationsService.deleteReservation(reservationId).subscribe({
        next: () => {
          alert('Reservation deleted successfully!');
          this.loadVehicles();
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.error('Error deleting reservation:', errorResponse);
          this.error = 'Failed to delete reservation. Please try again later.';
        },
      });
    }
  }

  returnVehicle(reservationId: string): void {
    if (confirm('Are you sure you want to return this vehicle?')) {
      this.vehicleService.returnVehicle(reservationId).subscribe({
        next: () => {
          alert('Vehicle returned successfully!');
          this.loadVehicles();
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.error('Error returning vehicle:', errorResponse);
          this.error = 'Failed to return vehicle. Please try again later.';
        },
      });
    }
  }
}
