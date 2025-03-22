import { Component, EventEmitter, Input, Output } from '@angular/core';
import { VehicleDetailsResponse } from '../../core/models/car.model';

@Component({
  selector: 'app-vehicle-details-modal',
  standalone: false,
  templateUrl: './vehicle-details-modal.component.html',
  styleUrl: './vehicle-details-modal.component.css',
})
export class VehicleDetailsModalComponent {
  @Input() vehicleDetails: VehicleDetailsResponse | null = null;
  @Input() error: string | null = null;
  @Input() isVisible: boolean = false;
  @Output() close = new EventEmitter<void>();

  onClose(): void {
    this.close.emit();
  }
}
