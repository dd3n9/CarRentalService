export interface Vehicle {
  vehicleId: string;
  brand: string;
  model: string;
  pricePerDay: number;
  city: string;
  year: number;
  seats: number;
}

export interface VehicleDetailsResponse {
  id: string;
  brand: string;
  model: string;
  type: string;
  licensePlate: string;
  year: number;
  seats: number;
  pricePerDay: number;
  isAvailable: boolean;
  rentalPointName: string;
  rentalPointAddress: string;
}
