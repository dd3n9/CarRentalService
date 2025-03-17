using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record VehicleYear
    {
        public int Value { get; }

        public VehicleYear(int value)
        {
            int currentYear = DateTime.UtcNow.Year;
            if (value < 1900 || value > currentYear)
            {
                throw new InvalidVehicleYearException();
            }
            Value = value;
        }

        public static implicit operator int(VehicleYear year) => year.Value;
        public static implicit operator VehicleYear(int year) => new(year);
    }
}
