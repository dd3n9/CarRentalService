namespace CarRentalService.Contracts.Vehicles.Requests
{
    public record CreateVehicleRequest(
        string VehicleBrand,
        string VehicleModel,
        decimal PricePerDay, 
        string VehicleType, 
        string LicensePlate, 
        int VehicleYear,
        int VehicleSeats, 
        Guid RentalPointId);
}
