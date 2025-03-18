namespace CarRentalService.Contracts.Reservations
{
    public record CreateReservationRequest(Guid VehicleId,
        Guid PickupPointId,
        Guid ReturnPointId, 
        DateTime StartDate,
        DateTime EndDate);
}
