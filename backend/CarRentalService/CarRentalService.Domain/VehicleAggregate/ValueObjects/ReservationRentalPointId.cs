using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record ReservationRentalPointId
    {
        public Guid Value { get; }

        public ReservationRentalPointId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyReservationRentalPointIdException();
            }
            Value = value;
        }

        public static ReservationRentalPointId CreateUnique()
        {
            return new ReservationRentalPointId(Guid.NewGuid());
        }

        public static implicit operator Guid(ReservationRentalPointId id)
            => id.Value;

        public static implicit operator ReservationRentalPointId(Guid id)
            => new(id);
    }
}
