namespace CarRentalService.Infrastructure.EF.Models
{
    internal class VehicleReadModel : BaseReadModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }

        public RentalPointReadModel RentalPoint { get; set; }
        public Guid RentalPointId { get; set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public ICollection<ReservationReadModel> Reservations { get; set; }
    }
}
