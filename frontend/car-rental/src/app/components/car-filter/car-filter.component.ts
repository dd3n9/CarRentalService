import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-car-filter',
  standalone: false,
  templateUrl: './car-filter.component.html',
  styleUrl: './car-filter.component.css',
})
export class CarFilterComponent {
  @Output() filtersChange = new EventEmitter<any>();

  city: string = '';
  vehicleType: string = '';
  startDate: Date | null = null;
  endDate: Date | null = null;
  yearFrom: number | null = null;
  yearTo: number | null = null;
  searchTerm: string = '';
  sortByPrice: string = '';
  vehicleTypes: string[] = ['Car', 'Truck', 'Motorcycle', 'Any'];

  constructor() {}

  ngOnInit(): void {}

  onFiltersUpdated(): void {
    const filters = {
      city: this.city,
      startDate: this.startDate,
      endDate: this.endDate,
      vehicleType: this.vehicleType === 'Any' ? '' : this.vehicleType,
      yearFrom: this.yearFrom,
      yearTo: this.yearTo,
      searchTerm: this.searchTerm,
      sortByPrice: this.sortByPrice,
    };
    this.filtersChange.emit(filters);
  }

  onResetFilters(): void {
    this.city = '';
    this.vehicleType = '';
    this.yearFrom = null;
    this.yearTo = null;
    this.searchTerm = '';
    this.sortByPrice = '';
    this.startDate = null;
    this.endDate = null;
    this.onFiltersUpdated();
  }
}
