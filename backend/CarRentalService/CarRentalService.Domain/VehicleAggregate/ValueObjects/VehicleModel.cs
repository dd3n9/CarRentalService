using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record VehicleModel
    {
        public string Value { get; }

        public VehicleModel(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyVehicleModelException();
            Value = value;
        }

        public static implicit operator VehicleModel(string value)
            => new(value);

        public static implicit operator string(VehicleModel value)
            => value.Value;
    }
}
