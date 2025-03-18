namespace CarRentalService.Contracts.Vehicles
{
    public record VehicleResponse(Guid VehicleId, 
        string Brand, 
        string Model, 
        double PricePerDay, 
        int Year, 
        int Seats
    );
}
