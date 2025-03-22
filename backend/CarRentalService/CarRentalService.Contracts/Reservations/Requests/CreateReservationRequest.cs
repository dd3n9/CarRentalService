namespace CarRentalService.Contracts.Reservations.Requests
{
    public record CreateReservationRequest(Guid VehicleId,
        Guid ReturnPointId,
        DateTime StartDate,
        DateTime EndDate);
}
