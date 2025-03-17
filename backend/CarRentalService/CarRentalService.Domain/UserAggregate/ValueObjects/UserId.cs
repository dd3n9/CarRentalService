using CarRentalService.Domain.Common.Exceptions.User;
using CarRentalService.Domain.Common.TypeConverters;
using System.ComponentModel;

namespace CarRentalService.Domain.UserAggregate.ValueObjects
{
    [TypeConverter(typeof(UserIdConverter))]
    public record UserId
    {
        public string Value { get; }

        public UserId(string value)
        {
            if (value is null)
                throw new EmptyApplicationUserIdException();
            Value = value;
        }

        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid().ToString());
        }

        public static implicit operator UserId(string id)
            => new UserId(id);

        public static implicit operator string(UserId value)
            => value.Value;

        public override string ToString()
            => Value;
    }
}
