using CarRentalService.Domain.Common.Exceptions.RentalPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.RentalPointAggregate.ValueObjects
{
    public record RentalPointAddress
    {
        public string Value { get; }

        public RentalPointAddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyRentalPointAddressException();
            Value = value;
        }

        public static implicit operator RentalPointAddress(string value)
            => new(value);

        public static implicit operator string(RentalPointAddress value)
            => value.Value;
    }
}
