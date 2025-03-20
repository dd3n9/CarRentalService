namespace CarRentalService.Contracts.Reservations.Requests
{
    public record CreateReservationRequest(Guid VehicleId,
        Guid PickupPointId,
        Guid ReturnPointId,
        DateTime StartDate,
        DateTime EndDate);
}
