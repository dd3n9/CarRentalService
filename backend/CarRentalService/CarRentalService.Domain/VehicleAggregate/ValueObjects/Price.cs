using CarRentalService.Domain.Common.Exceptions.Vehicle;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record Price
    {
        public decimal Value { get; }


        private Price(decimal value)
        {
            if (value <= 0)
                throw new InvalidPriceException();
            

            Value = value;
        }

        public static implicit operator decimal(Price value)
            => value.Value;

        public static implicit operator Price(decimal value)
            => new Price(value);
    }
}
