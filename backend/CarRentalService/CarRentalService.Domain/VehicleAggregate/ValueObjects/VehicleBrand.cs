using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record VehicleBrand
    {
        public string Value { get; }

        public VehicleBrand(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyVehicleBrandException();
            Value = value;
        }

        public static implicit operator VehicleBrand(string value)
            => new(value);

        public static implicit operator string(VehicleBrand value)
            => value.Value;
    }
}
