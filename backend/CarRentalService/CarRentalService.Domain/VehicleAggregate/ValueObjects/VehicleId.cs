
using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record VehicleId
    {
        public Guid Value { get; }

        public VehicleId(Guid value)
        {
            if (value == Guid.Empty)
                throw new EmptyVehicleIdException();

            Value = value;
        }

        public static VehicleId Create(Guid value)
        {
            return new VehicleId(value);
        }

        public static VehicleId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static implicit operator Guid(VehicleId value)
            => value.Value;

        public static implicit operator VehicleId(Guid value)
            => new VehicleId(value);
    }
}
