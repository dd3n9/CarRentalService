namespace CarRentalService.Infrastructure.EF.Models
{
    internal class VehicleReadModel : BaseReadModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; }
        public int Seats { get; set; }

        public RentalPointReadModel RentalPoint { get; set; }
        public Guid RentalPointId { get; set; }

        public ICollection<ReservationReadModel> Reservations { get; set; }
    }
}
