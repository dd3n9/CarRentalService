export interface Reservation {
  reservationId: string;
  vehicleBrand: string;
  vehicleModel: string;
  startDate: string;
  endDate: string;
  status: string;
  createdAt: string;
}

export interface CreateReservationRequest {
  vehicleId: string;
  returnPointId: string;
  startDate: string;
  endDate: string;
}
