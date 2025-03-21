import { Component, Input } from '@angular/core';
import { Vehicle } from '../../core/models/car.model';

@Component({
  selector: 'app-car-card',
  standalone: false,
  templateUrl: './car-card.component.html',
  styleUrl: './car-card.component.css',
})
export class CarCardComponent {
  @Input() vehicle!: Vehicle;

  constructor() {}

  ngOnInit(): void {}
}
