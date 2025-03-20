namespace CarRentalService.Contracts.Reservations.Responses
{
    public record ReservationResponse(Guid ReservationId, 
        string VehicleBrand, 
        string VehicleModel, 
        DateTime StartDate,
        DateTime EndDate, 
        string Status, 
        DateTime CreatedAt);
}
