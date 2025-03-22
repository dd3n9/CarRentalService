import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Vehicle, VehicleDetailsResponse } from '../../core/models/car.model';
import { VehicleService } from '../../core/services/vehicle.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-car-card',
  standalone: false,
  templateUrl: './car-card.component.html',
  styleUrl: './car-card.component.css',
})
export class CarCardComponent {
  @Input() vehicle!: Vehicle;
  @Output() viewDetails = new EventEmitter<string>();
  @Output() reserve = new EventEmitter<string>();

  showModal: boolean = false;
  vehicleDetails: VehicleDetailsResponse | null = null;
  error: string | null = null;

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {}

  onViewDetails(): void {
    this.viewDetails.emit(this.vehicle.vehicleId);
  }
  onReserve(): void {
    this.reserve.emit(this.vehicle.vehicleId);
  }

  closeModal(): void {
    this.showModal = false;
    this.vehicleDetails = null;
    this.error = null;
  }
}
