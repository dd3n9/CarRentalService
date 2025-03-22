import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VehicleService } from '../../core/services/vehicle.service';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { RentalpointsService } from '../../core/services/rentalpoints.service';
import { debounceTime, of, switchMap } from 'rxjs';
import { RentalPointSuggestion } from '../../core/models/rentalPoint.model';

@Component({
  selector: 'app-management-page',
  standalone: false,
  templateUrl: './management-page.component.html',
  styleUrl: './management-page.component.css',
})
export class ManagementPageComponent implements OnInit {
  reportForm: FormGroup;
  createVehicleForm: FormGroup;
  createRentalPointForm: FormGroup;

  vehicleIdToDelete: string = '';
  rentalPointIdToDelete: string = '';

  rentalPointSuggestionsForCreate: RentalPointSuggestion[] = [];
  rentalPointSuggestionsForDelete: RentalPointSuggestion[] = [];
  selectedRentalPointForCreate: RentalPointSuggestion | null = null;
  selectedRentalPointForDelete: RentalPointSuggestion | null = null;

  constructor(
    private fb: FormBuilder,
    private vehicleService: VehicleService,
    private rentalPointsService: RentalpointsService,
    private authService: AuthService,
    private router: Router
  ) {
    this.reportForm = this.fb.group({
      city: [''],
      yearFrom: [null],
      yearTo: [null],
      seats: [null],
      type: [''],
    });

    this.createVehicleForm = this.fb.group({
      vehicleBrand: ['', Validators.required],
      vehicleModel: ['', Validators.required],
      pricePerDay: [0, [Validators.required, Validators.min(0)]],
      vehicleType: ['', Validators.required],
      licensePlate: ['', Validators.required],
      vehicleYear: [0, [Validators.required, Validators.min(1900)]],
      vehicleSeats: [0, [Validators.required, Validators.min(1)]],
      rentalPointId: ['', Validators.required],
    });

    this.createRentalPointForm = this.fb.group({
      name: ['', Validators.required],
      city: ['', Validators.required],
      street: ['', Validators.required],
    });

    this.createVehicleForm
      .get('rentalPointId')!
      .valueChanges.pipe(
        debounceTime(300),
        switchMap((value) =>
          value
            ? this.rentalPointsService.getRentalPointSuggestions(value)
            : of([])
        )
      )
      .subscribe((suggestions: RentalPointSuggestion[]) => {
        this.rentalPointSuggestionsForCreate = suggestions;
      });
  }

  ngOnInit(): void {
    if (!this.authService.currentRoles.includes('MANAGER')) {
      this.router.navigate(['/cars']);
    }
  }

  selectRentalPointForCreate(suggestion: RentalPointSuggestion): void {
    this.selectedRentalPointForCreate = suggestion;
    this.createVehicleForm.get('rentalPointId')!.setValue(suggestion.id);
    this.rentalPointSuggestionsForCreate = [];
  }

  selectRentalPointForDelete(suggestion: RentalPointSuggestion): void {
    this.selectedRentalPointForDelete = suggestion;
    this.rentalPointIdToDelete = suggestion.id;
    this.rentalPointSuggestionsForDelete = [];
  }

  onBlurCreate(): void {
    setTimeout(() => {
      if (!this.selectedRentalPointForCreate) {
        this.rentalPointSuggestionsForCreate = [];
      }
    }, 200);
  }

  onBlurDelete(): void {
    setTimeout(() => {
      if (!this.selectedRentalPointForDelete) {
        this.rentalPointSuggestionsForDelete = [];
      }
    }, 200);
  }

  onRentalPointDeleteInput(event: Event): void {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.rentalPointIdToDelete = value;
    if (value) {
      this.rentalPointsService
        .getRentalPointSuggestions(value)
        .subscribe((suggestions) => {
          this.rentalPointSuggestionsForDelete = suggestions;
        });
    } else {
      this.rentalPointSuggestionsForDelete = [];
    }
  }

  generateReport(): void {
    const params = this.reportForm.value;
    this.vehicleService.getVehiclesReportPdf(params).subscribe({
      next: (blob: Blob) => {
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = 'VehiclesReport.pdf';
        link.click();
        window.URL.revokeObjectURL(url);
      },
      error: (err) => {
        console.error('Error generating report:', err);
        alert('Failed to generate report');
      },
    });
  }

  createVehicle(): void {
    if (this.createVehicleForm.invalid) return;

    const vehicleData = this.createVehicleForm.value;
    this.vehicleService.createVehicle(vehicleData).subscribe({
      next: (response) => {
        console.log('Vehicle created:', response);
        alert('Vehicle created successfully');
        this.createVehicleForm.reset();
        this.selectedRentalPointForCreate = null;
      },
      error: (err) => {
        console.error('Error creating vehicle:', err);
        alert('Failed to create vehicle');
      },
    });
  }

  deleteVehicle(): void {
    if (!this.vehicleIdToDelete) {
      alert('Please enter a vehicle ID');
      return;
    }

    this.vehicleService.deleteVehicle(this.vehicleIdToDelete).subscribe({
      next: () => {
        console.log('Vehicle deleted');
        alert('Vehicle deleted successfully');
        this.vehicleIdToDelete = '';
      },
      error: (err) => {
        console.error('Error deleting vehicle:', err);
        alert('Failed to delete vehicle');
      },
    });
  }

  createRentalPoint(): void {
    if (this.createRentalPointForm.invalid) return;

    const rentalPointData = this.createRentalPointForm.value;
    this.rentalPointsService.createRentalPoint(rentalPointData).subscribe({
      next: (response) => {
        console.log('Rental point created:', response);
        alert('Rental point created successfully');
        this.createRentalPointForm.reset();
      },
      error: (err) => {
        console.error('Error creating rental point:', err);
        alert('Failed to create rental point');
      },
    });
  }

  deleteRentalPoint(): void {
    if (!this.rentalPointIdToDelete || !this.selectedRentalPointForDelete) {
      alert('Please select a rental point to delete');
      return;
    }

    this.rentalPointsService
      .deleteRentalPoint(this.rentalPointIdToDelete)
      .subscribe({
        next: () => {
          console.log('Rental point deleted');
          alert('Rental point deleted successfully');
          this.rentalPointIdToDelete = '';
          this.selectedRentalPointForDelete = null;
          this.rentalPointSuggestionsForDelete = [];
        },
        error: (err) => {
          console.error('Error deleting rental point:', err);
          alert('Failed to delete rental point');
        },
      });
  }
}
