namespace CarRentalService.Infrastructure.EF.Models
{
    internal class ReservationReadModel
    {
        public Guid Id { get; set; }

        public UserReadModel User { get; set; }
        public string UserId { get; set; }

        public VehicleReadModel Vehicle { get; set; }
        public Guid VehicleId { get; set; }

        public RentalPointReadModel PickupPoint { get; set; }
        public Guid PickupPointId { get; set; }

        public RentalPointReadModel ReturnPoint { get; set; }
        public Guid ReturnPointId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string Status { get; set; }
    }
}
