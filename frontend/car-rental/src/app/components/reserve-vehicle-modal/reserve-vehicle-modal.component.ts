import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CreateReservationRequest } from '../../core/models/reservations.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RentalPointSuggestion } from '../../core/models/rentalPoint.model';
import { debounceTime, of, Subject, switchMap, takeUntil } from 'rxjs';
import { RentalpointsService } from '../../core/services/rentalpoints.service';

@Component({
  selector: 'app-reserve-vehicle-modal',
  standalone: false,
  templateUrl: './reserve-vehicle-modal.component.html',
  styleUrl: './reserve-vehicle-modal.component.css',
})
export class ReserveVehicleModalComponent implements OnInit {
  @Input() isVisible: boolean = false;
  @Input() vehicleId: string | null = null;
  @Input() error: string | null = null;
  @Output() close = new EventEmitter<void>();
  @Output() submitReservation = new EventEmitter<CreateReservationRequest>();

  reserveForm: FormGroup;
  returnPointSuggestions: RentalPointSuggestion[] = [];
  selectedReturnPoint: RentalPointSuggestion | null = null;

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private rentalPointsService: RentalpointsService
  ) {
    this.reserveForm = this.fb.group({
      returnPoint: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.reserveForm
      .get('returnPoint')!
      .valueChanges.pipe(
        debounceTime(300),
        switchMap((value) => {
          return value
            ? this.rentalPointsService.getRentalPointSuggestions(value)
            : of([]);
        }),
        takeUntil(this.destroy$)
      )
      .subscribe((suggestions: RentalPointSuggestion[]) => {
        this.returnPointSuggestions = suggestions;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  selectReturnPoint(suggestion: RentalPointSuggestion): void {
    this.selectedReturnPoint = suggestion;
    this.reserveForm
      .get('returnPoint')!
      .setValue(
        `${suggestion.rentalPointName} (${suggestion.rentalPointAddress})`
      );
    this.returnPointSuggestions = [];
  }

  onBlurReturnPoint(): void {
    setTimeout(() => {
      if (!this.selectedReturnPoint) {
        this.returnPointSuggestions = [];
      }
    }, 200);
  }

  onClose(): void {
    this.close.emit();
    this.resetForm();
  }

  onSubmit(): void {
    if (
      this.reserveForm.invalid ||
      !this.vehicleId ||
      !this.selectedReturnPoint
    ) {
      return;
    }

    const request: CreateReservationRequest = {
      vehicleId: this.vehicleId,
      returnPointId: this.selectedReturnPoint.id,
      startDate: this.reserveForm.get('startDate')!.value,
      endDate: this.reserveForm.get('endDate')!.value,
    };

    this.submitReservation.emit(request);
  }

  private resetForm(): void {
    this.reserveForm.reset();
    this.selectedReturnPoint = null;
    this.returnPointSuggestions = [];
  }
}
