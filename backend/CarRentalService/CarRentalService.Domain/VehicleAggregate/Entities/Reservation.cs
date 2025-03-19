using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;

namespace CarRentalService.Domain.VehicleAggregate.Entities
{
    public class Reservation : Entity<ReservationId>
    {
        public UserId UserId { get; private set; }
        public VehicleId VehicleId { get; private set; }
        public RentalPointId PickupPointId { get; private set; }
        public RentalPointId ReturnPointId { get; private set; }
        public ReservationDate StartDate { get; private set; }
        public ReservationDate EndDate { get; private set; }
        public ReservationDate? ReturnedDate { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        private Reservation() { }

        private Reservation(
            ReservationId reservationId,
            UserId userId,
            VehicleId vehicleId, 
            RentalPointId pickupPointId,
            RentalPointId returnPointId, 
            DateTime startDate,
            DateTime endDate,
            DateTime? returnedDate = null) : base(reservationId)
        {
            UserId = userId;
            VehicleId = vehicleId;
            PickupPointId = pickupPointId;
            ReturnPointId = returnPointId;
            StartDate = startDate;
            EndDate = endDate;
            ReturnedDate = returnedDate;
            Status = ReservationStatus.Active;
        }

        public static Reservation Create(
            UserId userId,
            VehicleId vehicleId,
            RentalPointId pickupPointId,
            RentalPointId returnPointId,
            DateTime startDate,
            DateTime endDate
            )
        {
            var reservation = new Reservation(ReservationId.CreateUnique(),
                userId, 
                vehicleId, 
                pickupPointId,
                returnPointId,
                startDate, 
                endDate);

            return reservation;
        }
    }
}
