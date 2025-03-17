using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record ReservationId
    {
        public Guid Value { get; }

        public ReservationId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyReservationIdException();
            }
            Value = value;
        }

        public static ReservationId CreateUnique()
        {
            return new ReservationId(Guid.NewGuid());
        }

        public static implicit operator Guid(ReservationId id)
            => id.Value;

        public static implicit operator ReservationId(Guid id)
            => new(id);
    }
}
