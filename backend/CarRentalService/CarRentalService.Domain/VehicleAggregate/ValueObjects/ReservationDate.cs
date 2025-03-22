using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record ReservationDate
    {
        public DateTime Value { get; }

        public ReservationDate(DateTime value)
        {
            if (value == DateTime.MinValue)
            {
                throw new InvalidReservationDateException();
            }

            Value = value;
        }

        public static implicit operator DateTime(ReservationDate date) => date.Value;
        public static implicit operator ReservationDate(DateTime date) => new(date);
    }
}
