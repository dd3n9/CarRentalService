namespace CarRentalService.Contracts.Vehicles
{
    public record VehicleResponse(Guid VehicleId, 
        string Brand, 
        string Model, 
        decimal PricePerDay, 
        string City,
        int Year, 
        int Seats
    );
}
