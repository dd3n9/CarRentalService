namespace CarRentalService.Contracts.Vehicles
{
    public record VehicleDetailsResponse(
        Guid Id,
        string Brand,
        string Model,
        string Type,
        string LicensePlate,
        int Year,
        int Seats,
        decimal PricePerDay,
        bool IsAvailable,
        string RentalPointName,
        string RentalPointAddress);
}
