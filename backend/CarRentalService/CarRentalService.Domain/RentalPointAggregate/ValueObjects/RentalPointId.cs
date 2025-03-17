using CarRentalService.Domain.Common.Exceptions.RentalPoint;

namespace CarRentalService.Domain.RentalPointAggregate.ValueObjects
{
    public record RentalPointId
    {
        public Guid Value { get; }

        public RentalPointId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyRentalPointIdException();
            }
            Value = value;
        }

        public static RentalPointId Create(Guid value)
        {
            return new RentalPointId(value);
        }

        public static RentalPointId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static implicit operator Guid(RentalPointId id)
            => id.Value;

        public static implicit operator RentalPointId(Guid id)
            => new(id);
    }
}
