namespace CarRentalService.Contracts.Common.DTOs
{
    public record ReservationDto(
        Guid Id,
        string UserId,
        Guid VehicleId,
        Guid PickupPointId,
        Guid ReturnPointId,
        DateTime StartDate,
        DateTime EndDate,
        DateTime? ReturnedDate,
        string Status
        );
}
