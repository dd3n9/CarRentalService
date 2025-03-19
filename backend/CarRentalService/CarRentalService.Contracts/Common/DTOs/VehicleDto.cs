namespace CarRentalService.Contracts.Common.DTOs
{
    public record VehicleDto(Guid Id, 
        string Brand,
        string Model,
        decimal PricePerDay,
        string Type,
        string LicensePlate,
        int Year,
        int Seats,
        string RentalCity,
        ICollection<ReservationDto> Reservations
        );
}
