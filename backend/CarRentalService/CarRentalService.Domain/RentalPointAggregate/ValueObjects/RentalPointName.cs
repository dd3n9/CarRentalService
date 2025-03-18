using CarRentalService.Domain.Common.Exceptions.RentalPoint;

namespace CarRentalService.Domain.RentalPointAggregate.ValueObjects
{
    public record RentalPointName
    {
        public string Value { get; }

        public RentalPointName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyRentalPointNameException();
            Value = value;
        }

        public static implicit operator RentalPointName(string value)
            => new(value);

        public static implicit operator string(RentalPointName value)
            => value.Value;
    }
}
