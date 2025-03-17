using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record VehicleSeats
    {
        public int Value { get; }

        private VehicleSeats(int value)
        {
            if (value < 1 || value > 65)
            {
                throw new InvalidVehicleSeatsCountException();
            }
            Value = value;
        }

        public static implicit operator int(VehicleSeats seats) => seats.Value;
        public static implicit operator VehicleSeats(int seats) => new(seats);
    }
}
