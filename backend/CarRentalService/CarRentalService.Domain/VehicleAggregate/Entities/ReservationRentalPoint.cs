using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;

namespace CarRentalService.Domain.VehicleAggregate.Entities
{
    public class ReservationRentalPoint : Entity<ReservationRentalPointId>
    {
        public ReservationId ReservationId { get; private set; }
        public RentalPointId RentalPointId { get; private set; }
        public RentalPointOperationType OperationType { get; private set; }

        private ReservationRentalPoint() { }

        private ReservationRentalPoint(
            ReservationRentalPointId id, 
            ReservationId reservationId, 
            RentalPointId rentalPointId,
            RentalPointOperationType operationType) : base(id)
        {
            ReservationId = reservationId;
            RentalPointId = rentalPointId;
            OperationType = operationType;
        }

        public ReservationRentalPoint Create(
            ReservationId reservationId,
            RentalPointId rentalPointId,
            RentalPointOperationType operationType)
        {
            var reservationRentalPoint = new ReservationRentalPoint(
                ReservationRentalPointId.CreateUnique(),
                reservationId, 
                rentalPointId,
                operationType
                );

            return reservationRentalPoint;
        }
    }
}
